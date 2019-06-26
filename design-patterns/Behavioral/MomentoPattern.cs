using System;
using System.Collections.Generic;
using System.Linq;

namespace design_patterns
{
    /*
     The pattern suggests storing the copy of the object’s state in a special object called memento. 
     The contents of the memento aren’t accessible to any other object except the one that produced it. 
     Other objects must communicate with mementos using a limited interface which may allow fetching 
     the snapshot’s metadata (creation time, the name of the performed operation, etc.), but not the original object’s state contained in the snapshot.
     */

    /// <summary>
    /// Memento is a behavioral design pattern that lets you save and restore the previous state of an object 
    /// without revealing the details of its implementation.
    /// </summary>
    class MomentoPattern : DesignPattern
    {
        public override string PatternName => "Memento";
        public override void ExecuteSample()
        {
            base.ExecuteSample();

            var editor = new Editor();
            var careTacker = new CareTaker(editor);

            editor.SetCursor(2, 4);
            editor.SetText("my custom text");

            careTacker.MakeBackup();

            editor.SetCursor(10, 14);
            editor.SetText("my free text");

            careTacker.Undo();

            Console.WriteLine($"Text: '{editor.GetText()}'");
        }
    }

    class CareTaker
    {
        private Editor _originator;

        private List<IMomento> _history;
        public CareTaker(Editor editor)
        {
            _originator = editor;
            _history = new List<IMomento>();
        }

        public void MakeBackup()
        {
            _history.Add(_originator.CreateSnaphot());
        }

        public void Undo()
        {
            if (_history.Any())
            {
                var latest = _history.OrderByDescending(r => r.GetSnaphotDate()).First();
                latest.Restore();
                _history.Remove(latest);
            }
        }
    }

    interface IMomento
    {
        string GetSnaphotName();
        DateTime GetSnaphotDate();
        void Restore();
    }

    class SnapshotMomento : IMomento
    {
        private Editor _originator;
        private string _text;
        private int _curX;
        private int _curY;

        private DateTime _dateCreated;

        public SnapshotMomento(Editor editor, string text, int curX, int curY)
        {
            _originator = editor;
            _text = text;
            _curX = curX;
            _curY = curY;
            _dateCreated = DateTime.Now;
        }

        public DateTime GetSnaphotDate()
        {
            return _dateCreated;
        }

        public string GetSnaphotName()
        {
            return _dateCreated.ToShortTimeString();
        }

        public void Restore()
        {
            _originator.SetText(_text);
            _originator.SetCursor(_curX, _curY);
        }
    }

    /// <summary>
    /// Momento originator
    /// </summary>
    class Editor
    {
        private string _text;
        private int _curX;
        private int _curY;

        public void SetText(string text)
        {
            _text = text;
        }

        public string GetText()
        {
            return _text;
        }

        public void SetCursor(int x, int y)
        {
            _curX = x;
            _curY = y;
        }

        public SnapshotMomento CreateSnaphot()
        {
            return new SnapshotMomento(this, _text, _curX, _curY);
        }
    }
}
