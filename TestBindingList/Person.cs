using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestBindingList
{
    public class Person : ViewModelBase
    {
        private string _name; public string Name { get => _name; set { Set(() => Name, ref _name, value); }}
        private int _age; public int Age { get => _age; set { Set(() => Age, ref _age, value); }}

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
