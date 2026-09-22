using System;
using System.Linq;
using VetClinic.Domain;
using VetClinic.Tests.Fixtures;
using Xunit;

namespace VetClinic.Tests;

/// <summary>
/// Класс отвечающий за тестирование связности домена
/// </summary>
public class ClinicIntegrityTests : IClassFixture<VetClinicFixture>
{
    private readonly VetClinicFixture _fixture;

    public ClinicIntegrityTests(VetClinicFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]

    /// <summary>
    /// Функция, проверяющая, заполнено ли поле владельца и животного и
    /// проверяющая соответсвие поля владельца в таблицах приёмов и питомцев
    /// </summary>

    public void EveryAppointmentShouldHaveValidAndMatchingPetAndOwner()
    {

        foreach (var appointment in _fixture.Appointments)
        {
            Assert.NotNull(appointment.Pet);
            Assert.NotNull(appointment.Pet.Owner);
            Assert.Equal(appointment.PetId, appointment.Pet.Id);
            Assert.Equal(appointment.Pet.OwnerId, appointment.Pet.Owner.Id);
        }
    }

    [Fact]
    /// <summary>
    /// Функция, проверяющая соответсвие породы животного и его вида
    /// </summary>
    public void PetSpeciesShouldStrictlyMatchItsBreedTargetSpecies()
    {
        foreach (var pet in _fixture.Pets)
        {
            Assert.NotNull(pet.Breed);
            Assert.Equal(pet.BreedId, pet.Breed.Id);
            Assert.Equal(pet.Type, pet.Breed.Type);
        }
    }

    [Fact]
    /// <summary>
    /// Функция, проверяющая соответствие специализации врача и вида животного
    /// </summary>
    public void AppointmentVetShouldBeSpecializedInAppointmentPetType()

    {
        foreach (var appointment in _fixture.Appointments)
        {
            Assert.NotNull(appointment.Vet);
            Assert.NotNull(appointment.Pet);
            Assert.Equal(appointment.Pet.Type, appointment.Vet.Specialization.TargetSpecies);
        }
    }
}

