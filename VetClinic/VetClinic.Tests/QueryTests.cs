using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;
using VetClinic.Tests.Fixtures;
using Xunit.Abstractions;

namespace VetClinic.Tests;
/// <summary>
/// Класс, имитирующий linq запросы к домену
/// </summary>
public class ClinicDomainTests(VetClinicFixture fixture, ITestOutputHelper output) : IClassFixture<VetClinicFixture>
{
    /// <summary>
    /// Поле с тестовыми данными
    /// </summary>
    private readonly VetClinicFixture _fixture = fixture;
    /// <summary>
    /// Поле с логами тестирвания
    /// </summary>
    private readonly ITestOutputHelper _output = output;

    /// <summary>
    /// Функция, которая распечатывает всех ветеринаров
    /// занимающихся кошками
    /// </summary>
    [Fact]
    public void FindAllVetsForSpecificAnimalType()
    {
        var vets = _fixture.Vets
            .Where(v => v.Specialization.TargetSpecies == Species.Cat)
            .ToList();

        foreach (Vet? v in vets)
        {
            _output.WriteLine($"ID: {v.Id} | Врач: {v.FullName} | Стаж: {v.ExpirienceYears} лет | Специализация: {v.Specialization}");
        }

        Assert.Equal(2, vets.Count);


    }

    [Fact]
    /// <summary>
    /// Функция, которая находит всех животных, которых принял 
    /// определённый врач, и сортирует в алфавитном порядке
    /// </summary>
    public void FindAllPetsForSpecificVetsAndSortByName()
    {
        Vet specificVet = _fixture.Vets[0];
        var matchedPets = _fixture.Appointments
            .Where(a => a.Vet == specificVet)
            .Select(a => a.Pet)
            .OrderBy(p => p.Name)
            .ToList();

        foreach (Pet? p in matchedPets)
        {
            _output.WriteLine($"  Питомец ID: {p.Id} | Кличка: {p.Name} | Вид: {p.Type} | Вес: {p.Weight} кг");
            _output.WriteLine($"  Дата рождения: {p.BirthDate}");
            _output.WriteLine($"  Порода: ID {p.BreedId} - {p.Breed.Title}");
            _output.WriteLine("");
        }
        Assert.Equal(2, matchedPets.Count);
        Assert.Contains(_fixture.Pets[0], matchedPets);
        Assert.Contains(_fixture.Pets[1], matchedPets);
    }

    /// <summary>
    /// Функция, которая находит количество повторных обращений 
    /// владельцев животных с выбранной породой
    /// </summary>
    [Fact]
    public void FindNumberOfSecondaryVisitForSpecificAnimalBreed()
    {
        Breed specificAnimalBreed = _fixture.Breeds[10];

        var matchedPets = _fixture.Appointments
            .Where(a => a.Pet.Breed == specificAnimalBreed && a.RepeatVisitIndicator == RepeatVisitIndicator.Yes)
            .Select(a => a.Pet)
            .OrderBy(p => p.Name)
            .ToList();

        foreach (Pet? p in matchedPets)
        {
            _output.WriteLine($"  Питомец ID: {p.Id} | Кличка: {p.Name} | Вид: {p.Type} | Вес: {p.Weight} кг");
            _output.WriteLine($"  Дата рождения: {p.BirthDate}");
            _output.WriteLine($"  Порода: ID {p.BreedId} - {p.Breed.Title}");
            _output.WriteLine("");
        }
        _output.WriteLine($" Колво питомцев:{matchedPets.Count}");
        Assert.NotEmpty(matchedPets);
        Assert.Single(matchedPets);
    }

    /// <summary>
    /// Функция, которая ищёт владельцев сразу нескольких питомцев
    /// </summary>
    [Fact]
    public void FindOwnersWithMultiplePets()
    {
        var owners = _fixture.Owners
            .Where(owner => _fixture.Pets.Count(pet => pet.Owner.Id == owner.Id) > 1)
            .OrderBy(owner => owner.FullName);
        foreach (Owner? o in owners)
        {
            _output.WriteLine($"ID: {o.Id} | Владелец: {o.FullName} | Телефон: {o.Phone} | Адрес: {o.Adress}");

        }
        Assert.NotEmpty(owners);
        Assert.Contains(_fixture.Owners[0], owners);
        Assert.Contains(_fixture.Owners[1], owners);
        Assert.Contains(_fixture.Owners[3], owners);
        Assert.Contains(_fixture.Owners[6], owners);
        Assert.Contains(_fixture.Owners[9], owners);
        Assert.Contains(_fixture.Owners[10], owners);
    }

    /// <summary>
    /// Функция, которая находит приёмы, проходившие в определённом кабинете в сентябре 2026
    /// </summary>
    [Fact]
    public void FindAppointmentsInSpecificRoomsInSpecificMonth()
    {
        var specificRoom = 102;
        var specificMonth = 9;

        Appointment[] appointments = [.. _fixture.Appointments
            .Where(appointment => appointment.RoomNumber == specificRoom &&
            appointment.DateTime.Year == 2026 && appointment.DateTime.Month == specificMonth)];
        foreach (Appointment? a in appointments)
        {
            _output.WriteLine($"Прием ID: {a.Id} | Дата: {a.DateTime} | Кабинет: {a.RoomNumber}");
            _output.WriteLine($"Повторный визит: {a.RepeatVisitIndicator} | Диагноз: {a.Diagnosis}");
            _output.WriteLine("");
        }
        Assert.NotEmpty(appointments);
        Assert.Contains(_fixture.Appointments[6], appointments);
        Assert.Contains(_fixture.Appointments[7], appointments);

    }
}

