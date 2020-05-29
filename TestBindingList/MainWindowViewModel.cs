using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections;

namespace TestBindingList
{

    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollectionEX<Person> _people = new ObservableCollectionEX<Person>();
        public ObservableCollectionEX<Person> People { get => _people; set { Set(() => People, ref _people, value); } }

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
                //updateLog($"Name:{person.Name} New Age:{person.Age}");
                //break;
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
            People.CollectionChanged += people_CollectionChanged;
            People.ItemPropertyChanged += people_ItemPropertyChanged;
            
            //People.ListChanged += people_ListChanged;
        }

        private void people_ItemPropertyChanged(IList SourceList, object Item, PropertyChangedEventArgs e)
        {
            updateLog($"Change Event: {e.PropertyName}");
        }

        private void people_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
            updateLog($"Change Event: {e.Action}");
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
            Person newPerson;
            for (int i=0; i<3;i++)
            {
                newPerson = new Person($"Person-{i}", i);
                //newPerson.PropertyChanged += newPerson_PropertyChanged;
                People.Add(newPerson);
            }
        }

        private void newPerson_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!(sender is Person person))
                return;

            updateLog($"Property Changed: Name:{person.Name} Age:{person.Age}");
        }
    }
}
