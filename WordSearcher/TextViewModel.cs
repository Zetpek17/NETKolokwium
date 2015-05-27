using System;
using System.Collections.Generic;
using System.Linq;

namespace WordSearcher
{
    /// <summary>
    /// Main class for View Model
    /// TODO: follow guidelines
    /// </summary>
    public class TextViewModel : ITextViewModel
    {
        private readonly IDispatcher _dispatcher;
        
        public TextViewModel(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            Content = Globals.LoremIpsum;
            _selected = new ASearcher();
        }

        private List<ASearcher> _lista = new List<ASearcher>();
        private ASearcher _selected;
        private string _content;
        private string _searchResult;
        private List<string> _keyWords = new List<string>();

        public string Query
        {
            get
            {
                return _selected.Query;
            }
            set
            {
                _selected.Query = value;
                //_dispatcher.RunOnUi();
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        public System.Windows.Input.ICommand SearchCommand
        {
            get {
                int find = 0;
                
                if (!string.IsNullOrEmpty(Query) && !string.IsNullOrEmpty(Content))
                {
                    _keyWords.Add(Content);
                    string[] result = Query.Split(' ');

                    foreach(string slowo in result)
                    {
                        if (SelectedMethod.VerifyText(slowo))
                            find++;
                    }
                }
                if (find <= 0)
                    SearchResult = "No result";
                else
                    SearchResult = "Results found: " + find;
                return null;
            }
        }

        public string SearchResult
        {
            get
            {
                return _searchResult;
            }
            set
            {
                _searchResult = value;
            }
        }

        public System.Windows.Input.ICommand SaveSearchesCommand
        {
            get {
                //return _keyWords;
                return null;
            }
        }

        public ASearcher SelectedMethod
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = SearchMethods.First();
            }
        }

        public IEnumerable<ASearcher> SearchMethods
        {
            get {
                _lista.Add(new ContainsSearcher());
                _lista.Add(new StartsWithSearcher());
                return _lista;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            System.ComponentModel.PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }
    }
}
