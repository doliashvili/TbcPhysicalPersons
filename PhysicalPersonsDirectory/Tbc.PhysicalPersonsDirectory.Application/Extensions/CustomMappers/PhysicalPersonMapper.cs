using Tbc.PhysicalPersonsDirectory.Application.Models;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Create;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.CreateRelation;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Commands.Update;
using Tbc.PhysicalPersonsDirectory.Application.PhysicalPersons.Queries.GetByIdIncludedData.Model;
using Tbc.PhysicalPersonsDirectory.Domain.Entities;
using RelatedPerson = Tbc.PhysicalPersonsDirectory.Domain.Entities.RelatedPerson;

namespace Tbc.PhysicalPersonsDirectory.Application.Extensions.CustomMappers
{
    public static class PhysicalPersonMapper
    {
        public static PhysicalPersonEntity MapToPhysicalPersonEntity(this CreatePhysicalPersonCommand self)
        {
            return new PhysicalPersonEntity
            {
                FirstName = self.FirstName,
                LastName = self.LastName,
                Gender = self.Gender,
                PersonalNumber = self.PersonalNumber,
                BirthDate = self.BirthDate,
                CityId = self.CityId,
                PhoneNumbers = self.PhoneNumbers?.ConvertAll(x => x.MapToPhoneNumberEntity())
            };
        }

        public static PhoneNumberEntity MapToPhoneNumberEntity(this PhoneNumberDto self)
        {
            return new PhoneNumberEntity
            {
                Number = self.Number,
                Type = self.Type
            };
        }

        public static GetByIdIncludedDataResponse MapToGetByIdIncludedDataResponse(this PhysicalPersonEntity self)
        {
            var physicalPersonDto = new GetByIdIncludedDataResponse
            {
                PhysicalPerson = new PhysicalPersonDto
                {
                    Id = self.Id,
                    FirstName = self.FirstName,
                    LastName = self.LastName,
                    Gender = self.Gender,
                    PersonalNumber = self.PersonalNumber,
                    BirthDate = self.BirthDate,
                    CityId = self.CityId,
                    PhoneNumbers = self.PhoneNumbers?.ConvertAll(x => new PhoneNumberDto { Number = x.Number, Type = x.Type }),
                    PicturePath = self.PicturePath,
                },

                RelationPersons = self.RelatedPersons?.ConvertAll(MapForRelation)
            };

            RelationPhysicalPersonDto MapForRelation(RelatedPerson relatedPerson)
            {
                if (relatedPerson is null || relatedPerson.RelatedEntity is null)
                {
                    return null;
                }

                return new RelationPhysicalPersonDto
                {
                    Id = relatedPerson.RelatedEntity.Id,
                    FirstName = relatedPerson.RelatedEntity.FirstName,
                    LastName = relatedPerson.RelatedEntity.LastName,
                    Gender = relatedPerson.RelatedEntity.Gender,
                    PersonalNumber = relatedPerson.RelatedEntity.PersonalNumber,
                    BirthDate = relatedPerson.RelatedEntity.BirthDate,
                    CityId = relatedPerson.RelatedEntity.CityId,
                    PhoneNumbers = relatedPerson.RelatedEntity.PhoneNumbers?.ConvertAll(x => new PhoneNumberDto { Number = x.Number, Type = x.Type }),
                    PicturePath = relatedPerson.RelatedEntity.PicturePath,
                    RelationshipType = relatedPerson.Relationship
                };
            }

            return physicalPersonDto;
        }

        public static PhysicalPersonDto MapToPhysicalPersonDtoResponse(this PhysicalPersonEntity self)
        {
            return new PhysicalPersonDto
            {
                Id = self.Id,
                FirstName = self.FirstName,
                LastName = self.LastName,
                Gender = self.Gender,
                PersonalNumber = self.PersonalNumber,
                BirthDate = self.BirthDate,
                CityId = self.CityId,
                PhoneNumbers = self.PhoneNumbers?.ConvertAll(x => new PhoneNumberDto { Number = x.Number, Type = x.Type }),
                PicturePath = self.PicturePath,
            };
        }

        public static RelatedPerson MapToRelatedPerson(this CreateRelationCommand self)
        {
            return new RelatedPerson
            {
                PhysicalPersonEntityId = self.MainPersonId,
                RelatedEntityId = self.RelationPersonId,
                Relationship = self.RelationshipType
            };
        }

        public static PhysicalPersonEntity MapUpdateToPhysicalPersonEntity(this UpdatePhysicalPersonCommand self, PhysicalPersonEntity entity)
        {
            entity.FirstName = self.FirstName;
            entity.LastName = self.LastName;
            entity.Gender = self.Gender;
            entity.PersonalNumber = self.PersonalNumber;
            entity.BirthDate = self.BirthDate;
            entity.CityId = self.CityId;
            entity.PhoneNumbers = self.PhoneNumbers?.ConvertAll(x => x.MapToPhoneNumberEntity());

            return entity;
        }
    }
}