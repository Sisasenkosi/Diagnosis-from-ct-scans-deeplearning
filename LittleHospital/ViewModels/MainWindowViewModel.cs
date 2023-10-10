using Prism.Mvvm;
using CadeCompetionML.Model;
using Microsoft.Win32;
using Prism.Commands;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CadeCompetion.modelclass;


namespace CadeCompetion.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {

            var testData = new ModelInput();
            testData.ImageSource = @"C:\Users\HP ZBook\Desktop\data\ct scan\TESTSSS\adenocarcinoma\000124 (6).png";
            ConsumeModel.Predict(testData);


        }


        #region properties
        private string imagePath;
        public string ImageSource
        {
            get { return imagePath; }
            set { SetProperty(ref imagePath, value); }
        }

        private List<Data> label;
        public List<Data> Labels
        {
            get { return label; }
            set { SetProperty(ref label, value); }
        }
        #endregion



        #region commands
        public DelegateCommand SelectComand => new DelegateCommand(ExecuteCommandName);

        // opening the folder wher you can select the image

        private void ExecuteCommandName()
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "jpeg Files(*.jpg;*jpeg)|.*.jpg;*jpeg|png Files (*.png)|*.png";
            if ((bool)diag.ShowDialog())
            {
                ImageSource = diag.FileName;

                ModelInput modelInput = new ModelInput();
                modelInput.ImageSource = diag.FileName;

                ModelOutput output = ConsumeModel.Predict(modelInput);

                var labels = File.ReadAllLines(@"C:\Users\HP ZBook\Desktop\sisa\CadeCompetion\labelElements.txt");

                List<Data> temp = new List<Data>();
                for (int i = 0; i < labels.Length; i++)
                {
                    Data data = new Data();
                    data.Label = labels[i];
                    data.Prediction = output.Score[i];
                    data.Percent = output.Score[i].ToString("P", CultureInfo.InvariantCulture);
                    temp.Add(data);

                }
                temp.Sort();

                Labels = temp;


                


            }


        }
        #endregion

    }
}
