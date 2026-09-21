using System;
using System.Linq;
using VetClinic.Domain;
using VetClinic.Tests.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace VetClinic.Tests;

public class ClinicDomainTests : IClassFixture<VetClinicFixture>
{
    private readonly VetClinicFixture _fixture;
    private readonly ITestOutputHelper _output; 
    public ClinicDomainTests(VetClinicFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }

    [Fact]
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
            _output.WriteLine($"  Специализация: {appointment.Vet.Specialization.Name}");
            _output.WriteLine("");

            _output.WriteLine("--- Вложенный объект Питомца (Pet) ---");
            _output.WriteLine($"  Питомец ID: {appointment.Pet.Id} | Кличка: {appointment.Pet.Name} | Вид: {appointment.Pet.Type} | Вес: {appointment.Pet.weight} кг");
            _output.WriteLine($"  Дата рождения: {appointment.Pet.BirthDate}");
            _output.WriteLine($"  Порода: ID {appointment.Pet.BreedId} - {appointment.Pet.Breed.Name}");
            _output.WriteLine("");

            _output.WriteLine("--- Вложенный объект Владельца (Owner) ---");
            _output.WriteLine($"  Владелец ID: {appointment.Pet.Owner.Id} | ФИО: {appointment.Pet.Owner.FullName}");
            _output.WriteLine($"  Телефон: {appointment.Pet.Owner.Phone} | Адрес: {appointment.Pet.Owner.Adress}");
            _output.WriteLine("========================================");
        }
        Assert.NotNull(appointments);
    }

    [Fact]
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
    public void FindAllPetsForSpecificVetsAndSort()
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
            _output.WriteLine($"  Порода: ID {p.BreedId} - {p.Breed.Name}");
            _output.WriteLine("");
        }
    }
    [Fact]
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
            _output.WriteLine($"  Порода: ID {p.BreedId} - {p.Breed.Name}");
            _output.WriteLine("");
        }
        _output.WriteLine($" Колво питомцев:{sortedPets.Count()}");
        Assert.NotEmpty(sortedPets);
        Assert.All(matchedAppointments, appointment=>Assert.True(appointment.RepeatVisitIndicator==RepeatVisitIndicator.Yes));
    }
    [Fact]
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
    public void FindAppointmentsInSpecificRooms()
    {
        var specificRoom = 102;
        
        
        var appointments = _fixture.Appointments
            .Where(appointment => appointment.RoomNumber== specificRoom && appointment.DateTime.Year == 2026 && appointment.DateTime.Month==9)
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

