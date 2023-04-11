using Application.PersonFeatures.Command.Add.CreatePersonCommand;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class PersonProfile:Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, AddPersonCommandModel>().ReverseMap();
            CreateMap<AddPersonCommandModel, Person>().ReverseMap();
        }
    }
}
