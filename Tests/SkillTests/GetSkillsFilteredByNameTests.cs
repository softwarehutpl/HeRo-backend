using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.SkillTests
{
    public class GetSkillsFilteredByNameTests : BaseSkillTests
    {
        /*
         * Czy wchodzi do metody?
         * Czy zwraca właśnie 5 lub mniej gdy zgadza się mniej?
         * Czy wszystkie zwrócone wartości zawierają podany ciąg znaków?
         * Czy przy podaniu napisu, który nie zawiera się w żadnej z nazw zwraca nic?
         */
        [Fact]
        public void GetSkillsFilteredByName_WhenEverythingIsOk_ShouldReturnAllMatchingSkills()
        {
            //arrange
            string name = "C";
            IQueryable<Skill> skills = new List<Skill>()
            {
                new Skill(1,"C"),
                new Skill(2, "C++"),
                new Skill(3, "C#"),
                new Skill(4, "Python"),
                new Skill(5, "Java")
            }.AsQueryable<Skill>();

            //act


            //assert
        }
    }
}
