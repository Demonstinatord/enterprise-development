using VetClinic.Domain;

namespace VetClinic.Tests.Fixtures;

public class VetClinicFixture
{
    public List<Breed> Breeds { get; }
    public List<Owner> Owners { get; }
    public List<Specialization> Specializations { get; }
    public List<Vet> Vets { get; }
    public List<Service> Services { get; }
    public List<Pet> Pets { get; }
    public List<Appointment> Appointments { get; }

    public VetClinicFixture()
    {
        // 1. Сначала жестко объявляем независимые справочники
        Breeds = GetBreedsList();
        Owners = GetOwnersList();
        Specializations = GetSpecializationsList();
        // 2. Затем объявляем врачей и услуги (они зависят только от enum Species)
        Vets = GetVetsList();
        Services = GetServicesList();

        // 3. Питомцы собираются матрешкой из Владельцев и Пород
        Pets = GetPetsList();

        // 4. Приемы связывают воедино Питомцев, Врачей и Услуги
        Appointments = GetAppointmentsList();
    }

    private List<Breed> GetBreedsList() => [

    new Breed { Id = 0, Name = "Мейн-кун", Type = Species.Cat },
    new Breed { Id = 1, Name = "Сиамская", Type = Species.Cat },
    new Breed { Id = 2, Name = "Британская короткошерстная", Type = Species.Cat },

    new Breed { Id = 3, Name = "Немецкая овчарка", Type = Species.Dog },
    new Breed { Id = 4, Name = "Лабрадор-ретривер", Type = Species.Dog },
    new Breed { Id = 5, Name = "Джек-рассел-терьер", Type = Species.Dog },

    new Breed { Id = 6, Name = "Сизый голубь", Type = Species.Pigeon },
    new Breed { Id = 7, Name = "Почтовый белый", Type = Species.Pigeon },
    new Breed { Id = 8, Name = "Якобин", Type = Species.Pigeon },

    new Breed { Id = 9, Name = "Ангорский кролик", Type = Species.Rabbit },
    new Breed { Id = 10, Name = "Голландский вислоухий", Type = Species.Rabbit },
    new Breed { Id = 11, Name = "Калифорнийский кролик", Type = Species.Rabbit },

    new Breed { Id = 12, Name = "Орловский рысак", Type = Species.Horse },
    new Breed { Id = 13, Name = "Арабская чистокровная", Type = Species.Horse },
    new Breed { Id = 14, Name = "Фризская", Type = Species.Horse }
];


    private List<Owner> GetOwnersList() => [
        new Owner { Id = 0, FullName = "Иванов Иван Иванович", Phone = "+7 (999) 111-22-33", Adress = "ул. Ленина, д. 10" },
        new Owner { Id = 1, FullName = "Петрова Анна Сергеевна", Phone = "+7 (999) 444-55-66", Adress = "ул. Садовая, д. 24" },
        new Owner { Id = 2, FullName = "Сидоров Пётр Петрович", Phone = "+7 (999) 777-88-99", Adress = "пр. Мира, д. 5" },
        new Owner { Id = 3, FullName = "Кузнецова Ольга Владимировна", Phone = "+7 (999) 222-33-44", Adress = "ул. Гагарина, д. 12" },
        new Owner { Id = 4, FullName = "Смирнов Дмитрий Александрович", Phone = "+7 (999) 555-66-77", Adress = "ул. Чехова, д. 8" },
        new Owner { Id = 5, FullName = "Васильева Елена Николаевна", Phone = "+7 (999) 888-99-00", Adress = "ул. Пушкина, д. 15" },
        new Owner { Id = 6, FullName = "Попов Сергей Игоревич", Phone = "+7 (999) 333-44-55", Adress = "ул. Новая, д. 3" },
        new Owner { Id = 7, FullName = "Михайлова Татьяна Юрьевна", Phone = "+7 (999) 666-77-88", Adress = "ул. Полевая, д. 21" },
        new Owner { Id = 8, FullName = "Федоров Андрей Олегович", Phone = "+7 (999) 999-00-11", Adress = "ул. Лесная, д. 4" },
        new Owner { Id = 9, FullName = "Морозова Наталья Сергеевна", Phone = "+7 (999) 123-98-76", Adress = "ул. Набережная, д. 30" },
        new Owner { Id = 10, FullName = "Иванов Сергей Викторович", Phone = "+7 (999) 477-55-66", Adress = "ул. Первомайская, д. 23" }
    ];

    private List<Specialization> GetSpecializationsList() => [

    new Specialization { Id = 0, Name = "Терапевт-фелинолог", TargetSpecies = Species.Cat },
    new Specialization { Id = 1, Name = "Хирург-онколог кошачьих отделений", TargetSpecies = Species.Cat },

    new Specialization { Id = 2, Name = "Кинолог-терапевт", TargetSpecies = Species.Dog },
    new Specialization { Id = 3, Name = "Травматолог-ортопед собак крупных пород", TargetSpecies = Species.Dog },

    new Specialization { Id = 4, Name = "Ветеринар-орнитолог", TargetSpecies = Species.Pigeon },
    new Specialization { Id = 5, Name = "Хирург-авиарлог", TargetSpecies = Species.Pigeon },

    new Specialization { Id = 6, Name = "Ветеринар-ратолог", TargetSpecies = Species.Rabbit },
    new Specialization { Id = 7, Name = "Стоматолог-родентолог", TargetSpecies = Species.Rabbit },

    // Лошади (Species.Horse)
    new Specialization { Id = 8, Name = "Ветеринар-ипполог", TargetSpecies = Species.Horse },
    new Specialization { Id = 9, Name = "Ортопед-подолог", TargetSpecies = Species.Horse }
];

    private List<Vet> GetVetsList() => [


        new Vet {
            Id = 0,
            FullName = "Др. Алексей Айболит",
            PassportNumber = "4511 111111",
            BirthDate = new DateOnly(1980, 5, 12),
            ExpirienceYears = 20, SpecializationId = 0,
            Specialization = Specializations[0]
        },

        new Vet {
            Id = 1,
            FullName = "Др. Степан Котов",
            PassportNumber = "4511 222222",
            BirthDate = new DateOnly(1990, 8, 14),
            ExpirienceYears = 6, SpecializationId = 1,
            Specialization = Specializations[1]
        },

        new Vet {
            Id = 2,
            FullName = "Др. Елена Быкова",
            PassportNumber = "4512 333333",
            BirthDate = new DateOnly(1985, 11, 23),
            ExpirienceYears = 12, SpecializationId = 2,
            Specialization = Specializations[2]
        },

        new Vet {
            Id = 3,
            FullName = "Др. Михаил Шариков",
            PassportNumber = "4512 444444",
            BirthDate = new DateOnly(1993, 1, 19),
            ExpirienceYears = 4,
            SpecializationId = 3,
            Specialization = Specializations[3]
        },

        new Vet {
            Id = 4,
            FullName = "Др. Анна Смит",
            PassportNumber = "4514 777777",
            BirthDate = new DateOnly(1992, 7, 4),
            ExpirienceYears = 6,
            SpecializationId = 4,
            Specialization = Specializations[4]
        },

        new Vet {
            Id = 5,
            FullName = "Др. Григорий Птицын",
            PassportNumber = "4514 888888",
            BirthDate = new DateOnly(1983, 4, 27),
            ExpirienceYears = 15,
            SpecializationId = 5,
            Specialization = Specializations[5]
        },

        new Vet { Id = 6, FullName = "Др. Кэррот Роджерс", PassportNumber = "4515 999999", BirthDate = new DateOnly(1988, 9, 30), ExpirienceYears = 10, SpecializationId = 6, Specialization = Specializations[6] },
        new Vet { Id = 7, FullName = "Др. Ольга Ушастая", PassportNumber = "4515 000000", BirthDate = new DateOnly(1995, 2, 10), ExpirienceYears = 3, SpecializationId = 7, Specialization = Specializations[7] },
        new Vet { Id = 8, FullName = "Др. Иван Дулиттл", PassportNumber = "4513 555555", BirthDate = new DateOnly(1978, 3, 15), ExpirienceYears = 25, SpecializationId = 8, Specialization = Specializations[8] },
        new Vet { Id = 9, FullName = "Др. Ксения Конева", PassportNumber = "4513 666666", BirthDate = new DateOnly(1989, 12, 5), ExpirienceYears = 9, SpecializationId = 9, Specialization = Specializations[9] }
];



    private List<Service> GetServicesList()
    {
        var s = Specializations;

        return [
            // Услуги для кошек (SpecializationId = 1 - Терапевт-фелинолог)
            new Service { Id = 0, Title = "Первичный осмотр и термометрия кошек", Price = 1000m, SpecializationId = 1, Specialization = s[0] },
        new Service { Id = 1, Title = "Комплексная вакцинация кошачьих", Price = 1500m, SpecializationId = 1, Specialization = s[0] },
        new Service { Id = 2, Title = "Гигиеническая чистка зубов кошкам", Price = 1800m, SpecializationId = 1, Specialization = s[0] },

        // Услуги для собак (SpecializationId = 3 - Кинолог-терапевт)
        new Service { Id = 3, Title = "Клинический осмотр и консультация собак", Price = 1200m, SpecializationId = 3, Specialization = s[2] },
        new Service { Id = 4, Title = "Комплексная вакцинация собак (с бешенством)", Price = 1700m, SpecializationId = 3, Specialization = s[2] },
        new Service { Id = 5, Title = "УЗИ органов брюшной полости собак", Price = 2500m, SpecializationId = 3, Specialization = s[2] },

        // Услуги для голубей / птиц (SpecializationId = 5 - Ветеринар-орнитолог)
        new Service { Id = 6, Title = "Осмотр птицы и микроскопия мазка", Price = 900m, SpecializationId = 5, Specialization = s[4] },
        new Service { Id = 7, Title = "Коррекция (подрезка) переросшего клюва", Price = 800m, SpecializationId = 5, Specialization = s[4] },
        new Service { Id = 8, Title = "Лабораторный анализ оперения на паразитов", Price = 1100m, SpecializationId = 5, Specialization = s[4] },

        // Услуги для кроликов (SpecializationId = 7 - Ветеринар-ратолог)
        new Service { Id = 9, Title = "Прием ратолога и визуальная диагностика", Price = 950m, SpecializationId = 7, Specialization = s[6] },
        new Service { Id = 10, Title = "Коррекция и подпилка резцов у грызунов", Price = 1300m, SpecializationId = 7, Specialization = s[6] },
        new Service { Id = 11, Title = "Вакцинация кроликов от ВГБК и миксоматоза", Price = 1100m, SpecializationId = 7, Specialization = s[6] },

        // Услуги для лошадей (SpecializationId = 9 - Ветеринар-ипполог)
        new Service { Id = 12, Title = "Выездной осмотр и аускультация лошади", Price = 4000m, SpecializationId = 9, Specialization = s[8] },
        new Service { Id = 13, Title = "Расчистка и ортопедическая обработка копыт", Price = 5000m, SpecializationId = 9, Specialization = s[8] },
        new Service { Id = 14, Title = "Рентгенография конечностей лошади", Price = 6500m, SpecializationId = 9, Specialization = s[8] }
        ];
    }


    private List<Pet> GetPetsList()
    {
        var o = Owners;
        var b = Breeds;

        List<Pet> list = [
        new Pet { Id = 0, Name = "Барсик", Type = Species.Cat, BirthDate = new DateOnly(2022, 4, 10), weight = 6, BreedId = b[0].Id, Breed = b[0], OwnerId = o[0].Id, Owner = o[0] },
        new Pet { Id = 1, Name = "Мурка", Type = Species.Cat, BirthDate = new DateOnly(2023, 6, 15), weight = 4, BreedId = b[1].Id, Breed = b[1], OwnerId = o[1].Id, Owner = o[1] },
        new Pet { Id = 2, Name = "Симба", Type = Species.Cat, BirthDate = new DateOnly(2025, 1, 20), weight = 5, BreedId = b[2].Id, Breed = b[2], OwnerId = o[2].Id, Owner = o[2] },


        new Pet { Id = 3, Name = "Шарик", Type = Species.Dog, BirthDate = new DateOnly(2020, 11, 5), weight = 30, BreedId = b[3].Id, Breed = b[3], OwnerId = o[0].Id, Owner = o[0] }, // У Владельца 0 два питомца
        new Pet { Id = 4, Name = "Рекс", Type = Species.Dog, BirthDate = new DateOnly(2021, 8, 25), weight = 28, BreedId = b[4].Id, Breed = b[4], OwnerId = o[3].Id, Owner = o[3] },
        new Pet { Id = 5, Name = "Граф", Type = Species.Dog, BirthDate = new DateOnly(2024, 3, 14), weight = 8, BreedId = b[5].Id, Breed = b[5], OwnerId = o[4].Id, Owner = o[4] },

        new Pet { Id = 6, Name = "Кеша", Type = Species.Pigeon, BirthDate = new DateOnly(2025, 5, 12), weight = 1, BreedId = b[6].Id, Breed = b[6], OwnerId = o[5].Id, Owner = o[5] },
        new Pet { Id = 7, Name = "Ворчун", Type = Species.Pigeon, BirthDate = new DateOnly(2024, 7, 19), weight = 1, BreedId = b[7].Id, Breed = b[7], OwnerId = o[6].Id, Owner = o[6] },
        new Pet { Id = 8, Name = "Пират", Type = Species.Pigeon, BirthDate = new DateOnly(2026, 2, 1), weight = 1, BreedId = b[8].Id, Breed = b[8], OwnerId = o[7].Id, Owner = o[7] },

        
        new Pet { Id = 9, Name = "Пушок", Type = Species.Rabbit, BirthDate = new DateOnly(2023, 10, 1), weight = 3, BreedId = b[9].Id, Breed = b[9], OwnerId = o[1].Id, Owner = o[1] }, // У Владельца 1 два питомца
        new Pet { Id = 10, Name = "Снежок", Type = Species.Rabbit, BirthDate = new DateOnly(2024, 12, 11), weight = 2, BreedId = b[10].Id, Breed = b[10], OwnerId = o[8].Id, Owner = o[8] },
        new Pet { Id = 11, Name = "Банни", Type = Species.Rabbit, BirthDate = new DateOnly(2025, 4, 3), weight = 4, BreedId = b[11].Id, Breed = b[11], OwnerId = o[9].Id, Owner = o[9] },

       
        new Pet { Id = 12, Name = "Буцефал", Type = Species.Horse, BirthDate = new DateOnly(2019, 5, 20), weight = 450, BreedId = b[12].Id, Breed = b[12], OwnerId = o[3].Id, Owner = o[3] }, // У Владельца 3 два питомца
        new Pet { Id = 13, Name = "Искра", Type = Species.Horse, BirthDate = new DateOnly(2021, 9, 15), weight = 420, BreedId = b[13].Id, Breed = b[13], OwnerId = o[6].Id, Owner = o[6] },   // У Владельца 6 два питомца
        new Pet { Id = 14, Name = "Тайфун", Type = Species.Horse, BirthDate = new DateOnly(2020, 6, 30), weight = 510, BreedId = b[14].Id, Breed = b[14], OwnerId = o[9].Id, Owner = o[9] },   // У Владельца 9 два питомца
    
        
        new Pet { Id = 15, Name = "Мурзик", Type = Species.Cat, BirthDate = new DateOnly(2020, 4, 10), weight = 6, BreedId = b[0].Id, Breed = b[0], OwnerId = o[10].Id, Owner = o[10] },
        new Pet { Id = 16, Name = "Боцик", Type = Species.Horse, BirthDate = new DateOnly(2023, 9, 15), weight = 375, BreedId = b[13].Id, Breed = b[13], OwnerId = o[10].Id, Owner = o[10] }
        ];
        return list;
    }


    private List<Appointment> GetAppointmentsList()
    {
        var p = Pets;
        var v = Vets;
        var s = Services;
        return [
        new Appointment { Id = 0, DateTime = new DateTime(2026, 9, 1, 10, 0, 0), Diagnosis = "Здоров", RoomNumber = 101, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId = p[0].Id, Pet = p[0], VetId = v[0].Id, Vet = v[0], ServiceId = s[0].Id, Service = s[0] }, 
        new Appointment { Id = 1, DateTime = new DateTime(2026, 2, 2, 11, 30, 0), Diagnosis = "Здорова", RoomNumber = 103, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId = p[1].Id, Pet = p[1], VetId = v[1].Id, Vet = v[1], ServiceId = s[0].Id, Service = s[0] }, 
        new Appointment { Id = 2, DateTime = new DateTime(2026, 9, 8, 10, 30, 0), Diagnosis = "Зубной камень удален", RoomNumber = 101, RepeatVisitIndicator = RepeatVisitIndicator.Yes, PetId = p[1].Id, Pet = p[1], VetId = v[0].Id, Vet = v[0], ServiceId = s[1].Id, Service = s[1] },
        new Appointment { Id = 3, DateTime = new DateTime(2026, 9, 12, 15, 45, 0), Diagnosis = "Витамины прописаны", RoomNumber = 103, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId = p[2].Id, Pet = p[2], VetId = v[1].Id, Vet = v[1], ServiceId = s[0].Id, Service = s[0] }, 

        new Appointment { Id = 4, DateTime = new DateTime(2026, 6, 3, 14, 0, 0), Diagnosis = "Здоров", RoomNumber = 102, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId = p[3].Id, Pet = p[3], VetId = v[2].Id, Vet = v[2], ServiceId = s[2].Id, Service = s[2] }, 
        new Appointment { Id = 5, DateTime = new DateTime(2026, 7, 4, 15, 0, 0), Diagnosis = "Назначена диета", RoomNumber = 106, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId = p[4].Id, Pet = p[4], VetId = v[3].Id, Vet = v[3], ServiceId = s[2].Id, Service = s[2] },
        new Appointment { Id = 6, DateTime = new DateTime(2026, 9, 9, 12, 0, 0), Diagnosis = "Плановое УЗИ", RoomNumber = 102, RepeatVisitIndicator = RepeatVisitIndicator.Yes, PetId = p[3].Id, Pet = p[3], VetId = v[2].Id, Vet = v[2], ServiceId = s[3].Id, Service = s[3] },
        new Appointment { Id = 7, DateTime = new DateTime(2026, 9, 13, 16, 0, 0), Diagnosis = "Вакцинация выполнена", RoomNumber = 102, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId = p[5].Id, Pet = p[5], VetId = v[2].Id, Vet = v[2], ServiceId = s[2].Id, Service = s[2] }, 

        new Appointment { Id = 8, DateTime = new DateTime(2026, 6, 6, 16, 30, 0), Diagnosis = "Здоров", RoomNumber = 105, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId = p[6].Id, Pet = p[6], VetId = v[4].Id, Vet = v[4], ServiceId = s[4].Id, Service = s[4] }, 
        new Appointment { Id = 9, DateTime = new DateTime(2026, 7, 14, 10, 0, 0), Diagnosis = "Клюв в норме", RoomNumber = 107, RepeatVisitIndicator = RepeatVisitIndicator.Yes, PetId = p[7].Id, Pet = p[7], VetId = v[5].Id, Vet = v[5], ServiceId = s[4].Id, Service = s[4] }, 
        new Appointment { Id = 10, DateTime = new DateTime(2026, 8, 16, 11, 0, 0), Diagnosis = "Осмотр крыла", RoomNumber = 105, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId = p[8].Id, Pet = p[8], VetId = v[4].Id, Vet = v[4], ServiceId = s[4].Id, Service = s[4] }, 

        new Appointment { Id = 11, DateTime = new DateTime(2026, 10, 7, 13, 0, 0), Diagnosis = "Отрастание резцов", RoomNumber = 104, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId = p[9].Id, Pet = p[9], VetId = v[6].Id, Vet = v[6], ServiceId = s[5].Id, Service = s[5] }, 
        new Appointment { Id = 12, DateTime = new DateTime(2026, 10, 15, 14, 0, 0), Diagnosis = "Зубы в порядке", RoomNumber = 108, RepeatVisitIndicator = RepeatVisitIndicator.Yes, PetId = p[10].Id, Pet = p[10], VetId = v[7].Id, Vet = v[7], ServiceId = s[5].Id, Service = s[5] },
        new Appointment { Id = 13, DateTime = new DateTime(2026, 10, 17, 09, 30, 0), Diagnosis = "Здоров", RoomNumber = 104, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId = p[11].Id, Pet = p[11], VetId = v[6].Id, Vet = v[6], ServiceId = s[5].Id, Service = s[5] }, 

        new Appointment { Id = 14, DateTime = new DateTime(2026, 9, 5, 9, 0, 0), Diagnosis = "Профилактика копыт", RoomNumber = 109, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId = p[12].Id, Pet = p[12], VetId = v[8].Id, Vet = v[8], ServiceId = s[6].Id, Service = s[6] },
        new Appointment { Id = 15, DateTime = new DateTime(2026, 9, 12, 9, 0, 0), Diagnosis = "Профилактика копыт", RoomNumber = 109, RepeatVisitIndicator = RepeatVisitIndicator.Not, PetId =p[16].Id, Pet=p[16], VetId = v[8].Id, Vet=v[8], ServiceId = s[6].Id, Service=s[6] },
        new Appointment { Id = 16, DateTime = new DateTime(2026, 8, 9, 11, 30, 0), Diagnosis = "Здоров", RoomNumber = 103, RepeatVisitIndicator = RepeatVisitIndicator.Yes, PetId = p[15].Id, Pet = p[15], VetId = v[1].Id, Vet = v[1], ServiceId = s[0].Id, Service = s[0] },
        ];
    }

}
