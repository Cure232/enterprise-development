using Agency.Application.Contracts.ContractRequests;
using Agency.Domain.Model;
using Bogus;

namespace Agency.Generator.Grpc.Host.Generator;

/// <summary>
/// Генератор тестовых ContractRequestCreateUpdateDto на основе Bogus
/// </summary>
public static class ContractRequestGenerator
{
    /// <summary>
    /// Генерация списка DTO для создания или обновления заявок
    /// </summary>
    public static IList<ContractRequestCreateUpdateDto> Generate(int count)
    {
        var faker = new Faker("ru");

        var list = new List<ContractRequestCreateUpdateDto>(count);

        var statuses = new[] { "Новая", "В обработке", "Одобрена", "Отклонена", "Завершена" };

        for (var i = 0; i < count; i++)
        {
            var counterpartyId = faker.Random.Int(1, 10);
            var realEstateId = faker.Random.Int(101, 110);
            var contractRequestType = faker.PickRandom<ContractRequestType>();
            var amount = Math.Round((decimal)faker.Random.Double(100_000, 100_000_000), 2);
            var createdDate = faker.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now);
            var status = faker.PickRandom(statuses);

            list.Add(new ContractRequestCreateUpdateDto(
                CounterpartyId: counterpartyId,
                RealEstateId: realEstateId,
                ContractRequestType: contractRequestType,
                Amount: amount,
                CreatedDate: createdDate,
                Status: status
            ));
        }

        return list;
    }
}
