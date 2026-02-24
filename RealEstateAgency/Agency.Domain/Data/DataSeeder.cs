using Agency.Domain.Model;

namespace Agency.Domain.Data;

/// <summary>
/// Данные для тестирования риэлторского агентства
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// Коллекция контрагентов (клиентов)
    /// </summary>
    public List<Counterparty> Counterparties { get; } = [];

    /// <summary>
    /// Коллекция объектов недвижимости
    /// </summary>
    public List<RealEstate> RealEstates { get; } = [];

    /// <summary>
    /// Коллекция заявок
    /// </summary>
    public List<ContractRequest> ContractRequests { get; } = [];

    /// <summary>
    /// Конструктор, выполняющий инициализацию всех тестовых данных
    /// </summary>
    public DataSeeder()
    {
        // Инициализация контрагентов (клиентов)
        Counterparties.AddRange(
            [
                new Counterparty
                {
                    Id = 1,
                    FullName = "Иванов Иван Иванович",
                    PassportNumber = "4501 123456",
                    PhoneNumber = "+7 (901) 123-45-67"
                },
                new Counterparty
                {
                    Id = 2,
                    FullName = "Петрова Анна Сергеевна",
                    PassportNumber = "4502 234567",
                    PhoneNumber = "+7 (902) 234-56-78"
                },
                new Counterparty
                {
                    Id = 3,
                    FullName = "Сидоров Петр Алексеевич",
                    PassportNumber = "4503 345678",
                    PhoneNumber = "+7 (903) 345-67-89"
                },
                new Counterparty
                {
                    Id = 4,
                    FullName = "Козлова Елена Дмитриевна",
                    PassportNumber = "4504 456789",
                    PhoneNumber = "+7 (904) 456-78-90"
                },
                new Counterparty
                {
                    Id = 5,
                    FullName = "Смирнов Алексей Владимирович",
                    PassportNumber = "4505 567890",
                    PhoneNumber = "+7 (905) 567-89-01"
                },
                new Counterparty
                {
                    Id = 6,
                    FullName = "Михайлова Ольга Игоревна",
                    PassportNumber = "4506 678901",
                    PhoneNumber = "+7 (906) 678-90-12"
                },
                new Counterparty
                {
                    Id = 7,
                    FullName = "Федоров Денис Андреевич",
                    PassportNumber = "4507 789012",
                    PhoneNumber = "+7 (907) 789-01-23"
                },
                new Counterparty
                {
                    Id = 8,
                    FullName = "Васильева Татьяна Павловна",
                    PassportNumber = "4508 890123",
                    PhoneNumber = "+7 (908) 890-12-34"
                },
                new Counterparty
                {
                    Id = 9,
                    FullName = "Николаев Артем Борисович",
                    PassportNumber = "4509 901234",
                    PhoneNumber = "+7 (909) 901-23-45"
                },
                new Counterparty
                {
                    Id = 10,
                    FullName = "Александрова Наталья Викторовна",
                    PassportNumber = "4510 012345",
                    PhoneNumber = "+7 (910) 012-34-56"
                }
            ]
        );

        // Инициализация объектов недвижимости
        RealEstates.AddRange(
            [
                new RealEstate
                {
                    Id = 101,
                    Type = RealEstateType.Apartment,
                    Purpose = RealEstatePurpose.Residential,
                    CadastralNumber = "77:01:0001012:1234",
                    Address = "г. Москва, ул. Ленина, д. 10, кв. 25",
                    TotalFloors = 9,
                    TotalArea = 65.5,
                    NumberOfRooms = 3,
                    CeilingHeight = 2.7,
                    Floor = 5,
                    HasEncumbrances = false
                },
                new RealEstate
                {
                    Id = 102,
                    Type = RealEstateType.Apartment,
                    Purpose = RealEstatePurpose.Residential,
                    CadastralNumber = "77:01:0001012:5678",
                    Address = "г. Москва, ул. Ленина, д. 10, кв. 42",
                    TotalFloors = 9,
                    TotalArea = 45.0,
                    NumberOfRooms = 2,
                    CeilingHeight = 2.7,
                    Floor = 3,
                    HasEncumbrances = true,
                    EncumbrancesDescription = "Ипотека Сбербанк"
                },
                new RealEstate
                {
                    Id = 103,
                    Type = RealEstateType.House,
                    Purpose = RealEstatePurpose.Residential,
                    CadastralNumber = "50:12:0030215:789",
                    Address = "Московская обл., Одинцовский р-н, д. Петрово, ул. Центральная, д. 15",
                    TotalFloors = 2,
                    TotalArea = 150.0,
                    NumberOfRooms = 5,
                    CeilingHeight = 3.2,
                    Floor = null,
                    HasEncumbrances = false
                },
                new RealEstate
                {
                    Id = 104,
                    Type = RealEstateType.LandPlot,
                    Purpose = RealEstatePurpose.Agricultural,
                    CadastralNumber = "50:12:0030215:123",
                    Address = "Московская обл., Одинцовский р-н, уч. 45",
                    TotalFloors = null,
                    TotalArea = 1200.0,
                    NumberOfRooms = null,
                    CeilingHeight = null,
                    Floor = null,
                    HasEncumbrances = false
                },
                new RealEstate
                {
                    Id = 105,
                    Type = RealEstateType.Commercial,
                    Purpose = RealEstatePurpose.Commercial,
                    CadastralNumber = "77:01:0003025:456",
                    Address = "г. Москва, ул. Тверская, д. 5, пом. 1",
                    TotalFloors = 3,
                    TotalArea = 250.0,
                    NumberOfRooms = 6,
                    CeilingHeight = 4.5,
                    Floor = 1,
                    HasEncumbrances = true,
                    EncumbrancesDescription = "Аренда до 2026 г."
                },
                new RealEstate
                {
                    Id = 106,
                    Type = RealEstateType.Garage,
                    Purpose = RealEstatePurpose.Residential,
                    CadastralNumber = "77:01:0004018:789",
                    Address = "г. Москва, ГСК 'Автомобилист', бокс 12",
                    TotalFloors = 1,
                    TotalArea = 20.0,
                    NumberOfRooms = null,
                    CeilingHeight = 2.5,
                    Floor = 1,
                    HasEncumbrances = false
                },
                new RealEstate
                {
                    Id = 107,
                    Type = RealEstateType.Apartment,
                    Purpose = RealEstatePurpose.Residential,
                    CadastralNumber = "78:01:0001034:567",
                    Address = "г. Санкт-Петербург, Невский пр., д. 25, кв. 12",
                    TotalFloors = 5,
                    TotalArea = 82.0,
                    NumberOfRooms = 4,
                    CeilingHeight = 3.0,
                    Floor = 2,
                    HasEncumbrances = false
                },
                new RealEstate
                {
                    Id = 108,
                    Type = RealEstateType.Apartment,
                    Purpose = RealEstatePurpose.Residential,
                    CadastralNumber = "78:01:0001034:890",
                    Address = "г. Санкт-Петербург, Невский пр., д. 25, кв. 15",
                    TotalFloors = 5,
                    TotalArea = 55.0,
                    NumberOfRooms = 2,
                    CeilingHeight = 3.0,
                    Floor = 4,
                    HasEncumbrances = false
                },
                new RealEstate
                {
                    Id = 109,
                    Type = RealEstateType.Commercial,
                    Purpose = RealEstatePurpose.Commercial,
                    CadastralNumber = "78:01:0002056:234",
                    Address = "г. Санкт-Петербург, ул. Рубинштейна, д. 10",
                    TotalFloors = 4,
                    TotalArea = 180.0,
                    NumberOfRooms = 4,
                    CeilingHeight = 3.5,
                    Floor = 1,
                    HasEncumbrances = false
                },
                new RealEstate
                {
                    Id = 110,
                    Type = RealEstateType.House,
                    Purpose = RealEstatePurpose.Residential,
                    CadastralNumber = "47:14:0030215:456",
                    Address = "Ленинградская обл., Всеволожский р-н, д. Романовка, ул. Лесная, д. 7",
                    TotalFloors = 2,
                    TotalArea = 120.0,
                    NumberOfRooms = 4,
                    CeilingHeight = 2.8,
                    Floor = null,
                    HasEncumbrances = true,
                    EncumbrancesDescription = "Залог"
                }
            ]
        );

        // Инициализация заявок
        ContractRequests.AddRange(
            [
                new ContractRequest
                {
                    Id = 201,
                    CounterpartyId = 1,
                    Counterparty = Counterparties[0], // Иванов
                    RealEstateId = 101,
                    RealEstate = RealEstates[0], // Квартира на Ленина, 25
                    ContractRequestType = ContractRequestType.Sale,
                    Amount = 12500000,
                    CreatedDate = new DateTime(2025, 1, 15),
                    Status = "Closed"
                },
                new ContractRequest
                {
                    Id = 202,
                    CounterpartyId = 2,
                    Counterparty = Counterparties[1], // Петрова
                    RealEstateId = 102,
                    RealEstate = RealEstates[1], // Квартира на Ленина, 42
                    ContractRequestType = ContractRequestType.Sale,
                    Amount = 9500000,
                    CreatedDate = new DateTime(2025, 1, 20),
                    Status = "Active"
                },
                new ContractRequest
                {
                    Id = 203,
                    CounterpartyId = 3,
                    Counterparty = Counterparties[2], // Сидоров
                    RealEstateId = 103,
                    RealEstate = RealEstates[2], // Дом в Петрово
                    ContractRequestType = ContractRequestType.Purchase,
                    Amount = 18500000,
                    CreatedDate = new DateTime(2025, 2, 5),
                    Status = "Active"
                },
                new ContractRequest
                {
                    Id = 204,
                    CounterpartyId = 1,
                    Counterparty = Counterparties[0], // Иванов (снова)
                    RealEstateId = 104,
                    RealEstate = RealEstates[3], // Участок
                    ContractRequestType = ContractRequestType.Purchase,
                    Amount = 3500000,
                    CreatedDate = new DateTime(2025, 2, 10),
                    Status = "Cancelled"
                },
                new ContractRequest
                {
                    Id = 205,
                    CounterpartyId = 4,
                    Counterparty = Counterparties[3], // Козлова
                    RealEstateId = 105,
                    RealEstate = RealEstates[4], // Коммерческое на Тверской
                    ContractRequestType = ContractRequestType.Purchase,
                    Amount = 45000000,
                    CreatedDate = new DateTime(2025, 3, 1),
                    Status = "Active"
                },
                new ContractRequest
                {
                    Id = 206,
                    CounterpartyId = 5,
                    Counterparty = Counterparties[4], // Смирнов
                    RealEstateId = 106,
                    RealEstate = RealEstates[5], // Гараж
                    ContractRequestType = ContractRequestType.Purchase,
                    Amount = 1200000,
                    CreatedDate = new DateTime(2025, 3, 15),
                    Status = "Active"
                },
                new ContractRequest
                {
                    Id = 207,
                    CounterpartyId = 6,
                    Counterparty = Counterparties[5], // Михайлова
                    RealEstateId = 107,
                    RealEstate = RealEstates[6], // Квартира на Невском, 12
                    ContractRequestType = ContractRequestType.Sale,
                    Amount = 16500000,
                    CreatedDate = new DateTime(2025, 4, 2),
                    Status = "Active"
                },
                new ContractRequest
                {
                    Id = 208,
                    CounterpartyId = 3,
                    Counterparty = Counterparties[2], // Сидоров (снова)
                    RealEstateId = 108,
                    RealEstate = RealEstates[7], // Квартира на Невском, 15
                    ContractRequestType = ContractRequestType.Purchase,
                    Amount = 11000000,
                    CreatedDate = new DateTime(2025, 4, 18),
                    Status = "Active"
                },
                new ContractRequest
                {
                    Id = 209,
                    CounterpartyId = 7,
                    Counterparty = Counterparties[6], // Федоров
                    RealEstateId = 109,
                    RealEstate = RealEstates[8], // Коммерческое на Рубинштейна
                    ContractRequestType = ContractRequestType.Sale,
                    Amount = 28000000,
                    CreatedDate = new DateTime(2025, 5, 5),
                    Status = "Active"
                },
                new ContractRequest
                {
                    Id = 210,
                    CounterpartyId = 8,
                    Counterparty = Counterparties[7], // Васильева
                    RealEstateId = 110,
                    RealEstate = RealEstates[9], // Дом в Романовке
                    ContractRequestType = ContractRequestType.Purchase,
                    Amount = 9500000,
                    CreatedDate = new DateTime(2025, 5, 20),
                    Status = "Active"
                },
                new ContractRequest
                {
                    Id = 211,
                    CounterpartyId = 9,
                    Counterparty = Counterparties[8], // Николаев
                    RealEstateId = 101,
                    RealEstate = RealEstates[0], // Квартира на Ленина, 25
                    ContractRequestType = ContractRequestType.Purchase,
                    Amount = 12500000,
                    CreatedDate = new DateTime(2025, 6, 1),
                    Status = "Active"
                },
                new ContractRequest
                {
                    Id = 212,
                    CounterpartyId = 2,
                    Counterparty = Counterparties[1], // Петрова (снова)
                    RealEstateId = 103,
                    RealEstate = RealEstates[2], // Дом в Петрово
                    ContractRequestType = ContractRequestType.Purchase,
                    Amount = 18000000,
                    CreatedDate = new DateTime(2025, 6, 15),
                    Status = "Active"
                },
                new ContractRequest
                {
                    Id = 213,
                    CounterpartyId = 10,
                    Counterparty = Counterparties[9], // Александрова
                    RealEstateId = 102,
                    RealEstate = RealEstates[1], // Квартира на Ленина, 42
                    ContractRequestType = ContractRequestType.Purchase,
                    Amount = 9200000,
                    CreatedDate = new DateTime(2025, 7, 1),
                    Status = "Active"
                }
            ]
        );

        // Устанавливаем навигационные свойства для каждого контрагента
        foreach (var counterparty in Counterparties)
        {
            counterparty.Requests = ContractRequests.Where(r => r.Counterparty.Id == counterparty.Id).ToList();
        }

        // Устанавливаем навигационные свойства для каждого объекта недвижимости
        foreach (var realEstate in RealEstates)
        {
            realEstate.Requests = ContractRequests.Where(r => r.RealEstate.Id == realEstate.Id).ToList();
        }
    }
}