using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;

namespace TestBindingList
{

    public class MainWindowViewModel : ViewModelBase
    {
        private BindingList<Person> _people = new BindingList<Person>();
        public BindingList<Person> People { get => _people; set { Set(() => People, ref _people, value); } }

        private string _log; public string Log { get => _log; set { Set(() => Log, ref _log, value); } }

        private RelayCommand _changeValuesCmd;
        public RelayCommand ChangeValuesCmd => _changeValuesCmd ?? (_changeValuesCmd = new RelayCommand(
            () => changeValues(),
            () => { return 1 == 1; },
            keepTargetAlive: true
            ));
        private void changeValues()
        {
            foreach (Person person in People)
            {
                person.Age++;
                updateLog($"Name:{person.Name} New Age:{person.Age}");
                break;
            }
        }


        private RelayCommand _resetLogCmd;
        public RelayCommand ResetLogCmd => _resetLogCmd ?? (_resetLogCmd = new RelayCommand(
            () => Log = string.Empty,
            () => { return 1 == 1; },
            keepTargetAlive: true
            ));


        private void updateLog(string v)
        {
            Log += $"{DateTime.Now.ToString()}: {v} \n";
        }

        public MainWindowViewModel()
        {
            loadSampleData();
            People.ListChanged += people_ListChanged;
        }

        private void people_ListChanged(object sender, ListChangedEventArgs e)
        {
            foreach (Person person in People)
            {
                //person.Name = $"* Age: {person.Age} *";
                updateLog($"Changed Person: {person.Name}");
            }
        }

        private void loadSampleData()
        {
            People.Add(new Person("uno", 1));
            People.Add(new Person("due", 2));
            People.Add(new Person("tre", 3));
        }
    }
}
