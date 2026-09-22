using System;
using System.Linq;
using VetClinic.Domain;
using VetClinic.Tests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace VetClinic.Tests;
/// <summary>
/// Класс, имитирующий linq запросы к домену
/// 
/// </summary>
public class ClinicDomainTests : IClassFixture<VetClinicFixture>
{
    /// <summary>
    /// Поле с тестовыми данными
    /// </summary>
    private readonly VetClinicFixture _fixture;
    /// <summary>
    /// Поле с логами тестирвания
    /// </summary>
    private readonly ITestOutputHelper _output; 
    public ClinicDomainTests(VetClinicFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }

    [Fact]
    /// <summary>
    /// Функция, которая распечатывает вложенную структуру Приёма -> (Врача; Питомца->Владельца)
    /// </summary>
    public void DemoShowMeDataStructure()
    {
        var appointments = _fixture.Appointments;
        _output.WriteLine("=== СТРУКТУРА ДАННЫХ ЗАПИСИ НА ПРИЕМ ===");
        foreach (var appointment in appointments)
        {
            
            _output.WriteLine($"Прием ID: {appointment.Id} | Дата: {appointment.DateTime} | Кабинет: {appointment.RoomNumber}");
            _output.WriteLine($"Повторный визит: {appointment.RepeatVisitIndicator} | Диагноз: {appointment.Diagnosis}");
            _output.WriteLine("");

            _output.WriteLine("--- Вложенный объект Врача (Vet) ---");
            _output.WriteLine($"  Врач ID: {appointment.Vet.Id} | ФИО: {appointment.Vet.FullName} | Стаж: {appointment.Vet.ExpirienceYears} лет");
            _output.WriteLine($"  Паспорт: {appointment.Vet.PassportNumber} | ДР: {appointment.Vet.BirthDate}");
            _output.WriteLine($"  Специализация: {appointment.Vet.Specialization.Title}");
            _output.WriteLine("");

            _output.WriteLine("--- Вложенный объект Питомца (Pet) ---");
            _output.WriteLine($"  Питомец ID: {appointment.Pet.Id} | Кличка: {appointment.Pet.Name} | Вид: {appointment.Pet.Type} | Вес: {appointment.Pet.weight} кг");
            _output.WriteLine($"  Дата рождения: {appointment.Pet.BirthDate}");
            _output.WriteLine($"  Порода: ID {appointment.Pet.BreedId} - {appointment.Pet.Breed.Title}");
            _output.WriteLine("");

            _output.WriteLine("--- Вложенный объект Владельца (Owner) ---");
            _output.WriteLine($"  Владелец ID: {appointment.Pet.Owner.Id} | ФИО: {appointment.Pet.Owner.FullName}");
            _output.WriteLine($"  Телефон: {appointment.Pet.Owner.Phone} | Адрес: {appointment.Pet.Owner.Adress}");
            _output.WriteLine("========================================");
        }
        Assert.NotNull(appointments);
    }

    [Fact]
    /// <summary>
    /// Функция, которая распечатывает всех ветеринаров
    /// занимающихся кошками
    /// </summary>
    public void FindAllVetsForSpecificAnimalType()
    {
        var vets = _fixture.Vets
            .Where(v => v.Specialization.TargetSpecies == Species.Cat)
            .ToList();

        foreach (var v in vets) { 
            _output.WriteLine($"ID: {v.Id} | Врач: {v.FullName} | Стаж: {v.ExpirienceYears} лет | Специализация: {v.Specialization}") ;
        }
        
        Assert.Equal(2,vets.Count());
        
    }

    [Fact]
    /// <summary>
    /// Функция, которая находит всех животных, которых принял 
    /// определённый врач, и сортирует в алфавитном порядке
    /// </summary>
    public void FindAllPetsForSpecificVetsAndSortByName()
    {
        var specificVet = _fixture.Vets[0];
        var matchedAppointments = _fixture.Appointments
            .Where(a => a.Vet == specificVet)
            .ToList();
        List<Pet> matchedPets=[];
        foreach (var a in matchedAppointments) {
            if (!matchedPets.Contains(a.Pet)) matchedPets.Add(a.Pet);
        }
        var sortedPets = matchedPets
            .OrderBy(p => p.Name)
            .ToList();
        foreach (var p in sortedPets)
        {
            _output.WriteLine($"  Питомец ID: {p.Id} | Кличка: {p.Name} | Вид: {p.Type} | Вес: {p.weight} кг");
            _output.WriteLine($"  Дата рождения: {p.BirthDate}");
            _output.WriteLine($"  Порода: ID {p.BreedId} - {p.Breed.Title}");
            _output.WriteLine("");
        }
        Assert.NotEmpty( sortedPets );
    }
    [Fact]
    /// <summary>
    /// Функция, которая находит количество повторных обращений 
    /// владельцев животных с выбранной породой
    /// </summary>
    public void FindNumberOfSecondaryVisitForSpecificAnimalBreed()
    {
        var specificAnimalBreed = _fixture.Breeds[10];
        
        var matchedAppointments = _fixture.Appointments
            .Where(a => a.Pet.Breed == specificAnimalBreed && a.RepeatVisitIndicator == RepeatVisitIndicator.Yes)
            .ToList();
        List<Pet> matchedPets = [];
        foreach (var a in matchedAppointments)
        {
            if (!matchedPets.Contains(a.Pet)) matchedPets.Add(a.Pet);
        }
        var sortedPets = matchedPets
            .OrderBy(p => p.Name)
            .ToList();
        foreach (var p in sortedPets)
        {
            _output.WriteLine($"  Питомец ID: {p.Id} | Кличка: {p.Name} | Вид: {p.Type} | Вес: {p.weight} кг");
            _output.WriteLine($"  Дата рождения: {p.BirthDate}");
            _output.WriteLine($"  Порода: ID {p.BreedId} - {p.Breed.Title}");
            _output.WriteLine("");
        }
        _output.WriteLine($" Колво питомцев:{sortedPets.Count()}");
        Assert.NotEmpty(sortedPets);
        Assert.All(matchedAppointments, appointment=>Assert.True(appointment.RepeatVisitIndicator==RepeatVisitIndicator.Yes));
    }
    [Fact]
    /// <summary>
    /// Функция, которая ищёт владельцев сразу нескольких питомцев
    /// </summary>
    public void FindOwnersWithMultiplePets()
    {
        var owners = _fixture.Owners
            .Where(owner => _fixture.Pets.Count(pet => pet.Owner.Id == owner.Id) > 1)
            .OrderBy(owner => owner.FullName)
            .ToArray();
        foreach (var o in owners)
        {
            _output.WriteLine($"ID: {o.Id} | Владелец: {o.FullName} | Телефон: {o.Phone} | Адрес: {o.Adress}");
        }
        Assert.NotEmpty(owners);

    }
    [Fact]
    /// <summary>
    /// Функция, которая находит приёмы, проходившие в определённом кабинете
    /// в сентябре 26 года
    public void FindAppointmentsInSpecificRoomsInSpecificMonth()
    {
        var specificRoom = 102;
        
        
        var appointments = _fixture.Appointments
            .Where(appointment => appointment.RoomNumber == specificRoom && appointment.DateTime.Year == 2026 && appointment.DateTime.Month==9)
            .ToArray();
        foreach (var a in appointments)
        {
            _output.WriteLine($"Прием ID: {a.Id} | Дата: {a.DateTime} | Кабинет: {a.RoomNumber}");
            _output.WriteLine($"Повторный визит: {a.RepeatVisitIndicator} | Диагноз: {a.Diagnosis}");
            _output.WriteLine("");
        }
        Assert.NotEmpty(appointments);

    }
}

