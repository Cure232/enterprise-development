using Agency.Domain.Data;
using Agency.Domain.Model;

namespace Agency.Tests;

/// <summary>
/// Тесты для доменной области риэлторского агентства
/// </summary>
public class RealEstateTests(DataSeeder data) : IClassFixture<DataSeeder>
{
    /// <summary>
    /// Проверяет, что корректно выводятся все продавцы, оставившие заявки на продажу за заданный период
    /// </summary>
    [Fact]
    public void SellersWithRequestsInPeriod()
    {
        // Arrange
        var from = new DateTime(2025, 1, 1);
        var to = new DateTime(2025, 3, 31);

        // Актуальные продавцы с заявками на продажу в 1 квартале 2025:
        // - Иванов (Id: 1) - заявка 201 (продажа, 15.01.2025)
        // - Петрова (Id: 2) - заявка 202 (продажа, 20.01.2025)
        // - Козлова (Id: 4) - заявка 205 (покупка, не продажа) - НЕ ДОЛЖНА ПОПАСТЬ
        // - Смирнов (Id: 5) - заявка 206 (покупка, не продажа) - НЕ ДОЛЖЕН ПОПАСТЬ
        // - Михайлова (Id: 6) - заявка 207 (продажа, 02.04.2025) - после периода
        // - Федоров (Id: 7) - заявка 209 (продажа, 05.05.2025) - после периода

        var expectedSellerIds = new List<int> { 1, 2 }; // Иванов и Петрова

        // Act
        var sellers = data.ContractRequests
            .Where(r => r.ContractRequestType == ContractRequestType.Sale
                        && r.CreatedDate >= from
                        && r.CreatedDate <= to)
            .Select(r => r.Counterparty)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        // Assert
        Assert.Equal(expectedSellerIds.Count, sellers.Count);
        Assert.All(expectedSellerIds, id => Assert.Contains(sellers, s => s.Id == id));
    }

    /// <summary>
    /// Проверяет, что корректно возвращаются топ 5 клиентов по количеству заявок на покупку
    /// </summary>
    [Fact]
    public void Top5BuyersByRequestCount()
    {
        // Подсчет заявок на покупку по клиентам:
        // Иванов (Id: 1) - 1 покупка (заявка 204)
        // Петрова (Id: 2) - 2 покупки (заявки 212, и еще одна?)
        // Сидоров (Id: 3) - 2 покупки (заявки 203, 208)
        // Козлова (Id: 4) - 1 покупка (заявка 205)
        // Смирнов (Id: 5) - 1 покупка (заявка 206)
        // Васильева (Id: 8) - 1 покупка (заявка 210)
        // Николаев (Id: 9) - 1 покупка (заявка 211)
        // Александрова (Id: 10) - 1 покупка (заявка 213)

        var buyersByCount = data.ContractRequests
            .Where(r => r.ContractRequestType == ContractRequestType.Purchase)
            .GroupBy(r => r.Counterparty)
            .Select(g => new { Counterparty = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ToList();

        // Топ-5 Id клиентов по количеству покупок
        var expectedBuyerIds = buyersByCount
            .Take(5)
            .Select(x => x.Counterparty.Id)
            .ToList();

        // Act
        var topBuyers = buyersByCount
            .Take(5)
            .Select(x => x.Counterparty)
            .ToList();

        // Assert
        Assert.Equal(5, topBuyers.Count);
        Assert.Equal(expectedBuyerIds, topBuyers.Select(b => b.Id).ToList());
    }

    /// <summary>
    /// Проверяет, что корректно возвращаются топ 5 клиентов по количеству заявок на продажу
    /// </summary>
    [Fact]
    public void Top5SellersByRequestCount()
    {
        // Подсчет заявок на продажу по клиентам:
        // Иванов (Id: 1) - 1 продажа (заявка 201)
        // Петрова (Id: 2) - 1 продажа (заявка 202)
        // Михайлова (Id: 6) - 1 продажа (заявка 207)
        // Федоров (Id: 7) - 1 продажа (заявка 209)

        var sellersByCount = data.ContractRequests
            .Where(r => r.ContractRequestType == ContractRequestType.Sale)
            .GroupBy(r => r.Counterparty)
            .Select(g => new { Counterparty = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ToList();

        // Топ Id клиентов по количеству продаж
        var expectedSellerIds = sellersByCount
            .Select(x => x.Counterparty.Id)
            .ToList();

        // Act
        var topSellers = sellersByCount
            .Take(5)
            .Select(x => x.Counterparty)
            .ToList();

        // Assert
        Assert.True(topSellers.Count <= 5);
        Assert.Equal(expectedSellerIds, topSellers.Select(s => s.Id).ToList());
    }

    /// <summary>
    /// Проверяет, что корректно выводится информация о количестве заявок по каждому типу недвижимости
    /// </summary>
    [Fact]
    public void RequestCountByPropertyType()
    {
        // Подсчет заявок по типам недвижимости:
        var expectedCounts = new Dictionary<RealEstateType, int>
        {
            { RealEstateType.Apartment, 6 },   // ID 101,102,107,108 - заявки: 201,202,207,208,211,213 (6 заявок)
            { RealEstateType.House, 3 },       // ID 103,110 - заявки: 203,210,212 (3 заявки)
            { RealEstateType.LandPlot, 1 },    // ID 104 - заявка: 204 (1 заявка)
            { RealEstateType.Commercial, 2 },   // ID 105,109 - заявки: 205,209 (2 заявки)
            { RealEstateType.Garage, 1 }        // ID 106 - заявка: 206 (1 заявка)
        };

        // Act
        var requestsByType = data.ContractRequests
            .GroupBy(r => r.RealEstate.Type)
            .Select(g => new { PropertyType = g.Key, Count = g.Count() })
            .ToDictionary(x => x.PropertyType, x => x.Count);

        // Assert
        Assert.Equal(expectedCounts.Count, requestsByType.Count);

        foreach (var type in expectedCounts.Keys)
        {
            Assert.True(requestsByType.ContainsKey(type), $"Отсутствует тип {type}");
            Assert.Equal(expectedCounts[type], requestsByType[type]);
        }
    }

    /// <summary>
    /// Проверяет, что корректно выводится информация о клиентах, открывших заявки с минимальной стоимостью
    /// </summary>
    [Fact]
    public void ClientsWithMinimalRequestAmount()
    {
        // Минимальная сумма заявки - 1 200 000 (гараж, заявка 206, клиент Смирнов Id: 5)
        var minAmount = data.ContractRequests.Min(r => r.Amount);
        var expectedAmount = 1200000m;
        var expectedClientIds = new List<int> { 5 }; // Смирнов

        // Act
        var clients = data.ContractRequests
            .Where(r => r.Amount == minAmount)
            .Select(r => r.Counterparty)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        // Assert
        Assert.Equal(expectedAmount, minAmount);
        Assert.Equal(expectedClientIds.Count, clients.Count);
        Assert.All(expectedClientIds, id => Assert.Contains(clients, c => c.Id == id));
    }

    /// <summary>
    /// Проверяет, что корректно выводятся сведения о всех клиентах, ищущих недвижимость заданного типа, упорядоченные по ФИО
    /// </summary>
    [Fact]
    public void ClientsSearchingForSpecificPropertyType()
    {
        // Arrange
        var propertyType = RealEstateType.Apartment;

        // Клиенты, ищущие квартиры (покупка):
        // - Иванов (Id: 1) - заявка 204 (участок, не квартира) - НЕ ДОЛЖЕН ПОПАСТЬ
        // - Сидоров (Id: 3) - заявка 208 (квартира)
        // - Петрова (Id: 2) - заявка 212 (дом, не квартира) - НЕ ДОЛЖНА ПОПАСТЬ
        // - Николаев (Id: 9) - заявка 211 (квартира)
        // - Александрова (Id: 10) - заявка 213 (квартира)
        // - Васильева (Id: 8) - заявка 210 (дом, не квартира) - НЕ ДОЛЖНА ПОПАСТЬ

        var expectedClientIds = new List<int> { 3, 9, 10 }; // Сидоров, Николаев, Александрова

        // Act
        var clients = data.ContractRequests
            .Where(r => r.ContractRequestType == ContractRequestType.Purchase
                        && r.RealEstate.Type == propertyType)
            .Select(r => r.Counterparty)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        // Assert
        Assert.Equal(expectedClientIds.Count, clients.Count);

        // Проверяем сортировку по ФИО
        var sortedNames = clients.Select(c => c.FullName).OrderBy(n => n).ToList();
        var actualNames = clients.Select(c => c.FullName).ToList();
        Assert.Equal(sortedNames, actualNames);

        // Проверяем, что все ожидаемые клиенты присутствуют
        Assert.All(expectedClientIds, id => Assert.Contains(clients, c => c.Id == id));
    }

    /// <summary>
    /// Проверяет, что корректно возвращаются клиенты, ищущие дома, упорядоченные по ФИО
    /// </summary>
    [Fact]
    public void ClientsSearchingForHousesOrderedByFullName()
    {
        // Arrange
        var propertyType = RealEstateType.House;

        // Клиенты, ищущие дома (покупка):
        // - Петрова (Id: 2) - заявка 212 (дом)
        // - Сидоров (Id: 3) - заявка 203 (дом)
        // - Васильева (Id: 8) - заявка 210 (дом)

        var expectedClientIds = new List<int> { 2, 3, 8 }; // Петрова, Сидоров, Васильева
        var expectedSortedNames = new List<string>
        {
            "Васильева Татьяна Павловна",
            "Петрова Анна Сергеевна",
            "Сидоров Петр Алексеевич"
        };

        // Act
        var clients = data.ContractRequests
            .Where(r => r.ContractRequestType == ContractRequestType.Purchase
                        && r.RealEstate.Type == propertyType)
            .Select(r => r.Counterparty)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        // Assert
        Assert.Equal(expectedClientIds.Count, clients.Count);

        // Проверяем сортировку по ФИО
        var actualNames = clients.Select(c => c.FullName).ToList();
        Assert.Equal(expectedSortedNames, actualNames);

        // Проверяем, что все ожидаемые клиенты присутствуют
        Assert.All(expectedClientIds, id => Assert.Contains(clients, c => c.Id == id));
    }
}