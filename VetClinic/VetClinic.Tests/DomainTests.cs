using System;
using System.Linq;
using VetClinic.Domain;
using VetClinic.Tests.Fixtures;
using Xunit;

namespace VetClinic.Tests;

public class ClinicIntegrityTests : IClassFixture<VetClinicFixture>
{
    private readonly VetClinicFixture _fixture;

    public ClinicIntegrityTests(VetClinicFixture fixture)
    {
        _fixture = fixture;
    }

    // 1. ПРОВЕРКА СВЯЗИ: Приём -> Питомец -> Владелец
    [Fact]
    public void Every_Appointment_Should_Have_Valid_And_Matching_Pet_And_Owner()
    {
        // Act & Assert — Проходим по всем 15 приёмам
        foreach (var appointment in _fixture.Appointments)
        {
            // Проверяем контракт обязательности: объекты не должны быть null
            Assert.NotNull(appointment.Pet);
            Assert.NotNull(appointment.Pet.Owner);

            // Проверяем целостность ID: PetId в приёме должен совпадать с фактическим Id объекта Pet
            Assert.Equal(appointment.PetId, appointment.Pet.Id);

            // Проверяем сквозную связь по ID: OwnerId в питомце должен совпадать с Id его владельца
            Assert.Equal(appointment.Pet.OwnerId, appointment.Pet.Owner.Id);
        }
    }

   
    // 2. ПРОВЕРКА БИЗНЕС-КОНТРАКТА: Вид животного (Species) и его Порода (Breed)
    [Fact]
    public void Pet_Species_Should_Strictly_Match_Its_Breed_TargetSpecies()
    {
        // Тест гарантирует, что "Немецкая овчарка" привязана только к Собакам, а "Мейн-кун" к Кошкам
        foreach (var pet in _fixture.Pets)
        {
            Assert.NotNull(pet.Breed);

            // Проверяем равенство идентификаторов
            Assert.Equal(pet.BreedId, pet.Breed.Id);

            // Проверяем, что тип питомца (Type) совпадает с целевым видом породы (TargetSpecies)
            Assert.Equal(pet.Type, pet.Breed.Type);
        }
    }

    // 3. ПРОВЕРКА СВЯЗИ: Приём -> Врач (Соответствие специализации)
    [Fact]
    public void AppointmentVetShouldBeSpecializedInAppointmentPetType()
    {
        // Проверяем, что логика подбора врача на приёме не нарушена
        foreach (var appointment in _fixture.Appointments)
        {
            Assert.NotNull(appointment.Vet);
            Assert.NotNull(appointment.Pet);

            // Вид принимаемого животного должен строго соответствовать специализации врача
            Assert.Equal(appointment.Pet.Type, appointment.Vet.Specialization.TargetSpecies);
        }
    }
}

