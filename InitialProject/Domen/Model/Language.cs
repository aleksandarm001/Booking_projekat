namespace InitialProject.Domen.Model
{
    using System;
    using System.IO.Packaging;

    public class Language : ValidationBase, ISerializable
    {
        private string name;
        public string Name
        {
            get { return name; }

            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public Language(string language)
        {
            Name = language;
        }
        public Language()
        {
            Name = "";
        }

        public override string ToString()
        {
            return Name;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Name
            };

            return csvValues;
        }

        public Language fromStringToLanguage(string s)
        {
            return new Language(s);
        }


        public void FromCSV(string[] values)
        {
            Name = values[0];
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                this.ValidationErrors["Name"] = "Language name should not be empty.";
            }
        }
    }
}
