using Acr.UserDialogs;
using MultiplicationTable.Resx;
using MultiplicationTable.Services;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MultiplicationTable.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = Language.btnAboutUpdate;
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://github.com/kansasdev"));

            ButtonEnabled = true;

            TimeoutSource = new ObservableCollection<int>();
            for(int i=1;i<=60;i++)
            {
                TimeoutSource.Add(i);
            }
            SumSource = new ObservableCollection<int>();
            for (int i = 1; i <= 50; i++)
            {
                SumSource.Add(i);
            }
            DiffSource = new ObservableCollection<int>();
            for (int i = 1; i <= 50; i++)
            {
                DiffSource.Add(i);
            }
            MultSource = new ObservableCollection<int>();
            for (int i = 1; i <= 10; i++)
            {
                MultSource.Add(i);
            }

            if (Settings.WorkMode=="" || Settings.WorkMode=="Normal")
            {
                Marked = false;
            }
            else
            {
                Marked = true;
            }

            SelectedDiff = Settings.DiffMax;
            SelectedMult = Settings.MultMax;
            SelectedSum = Settings.SumMax;
            SelectedTimeout = Settings.Timeout;

            SaveSettings = new Command(() =>
            {
                if(Marked)
                {
                    Settings.WorkMode = "Quiz";
                }
                else
                {
                    Settings.WorkMode = "Normal";
                }
                Settings.Timeout = SelectedTimeout;
                Settings.SumMax = SelectedSum;
                Settings.DiffMax = SelectedDiff;
                Settings.MultMax = SelectedMult;
             
            });

            LoadUserDefinedDict = new Command(new Action(LoadUserDefinedDictAction));
            LoadUserDefinedWord = new Command(new Action(LoadUserDefinedWordAction));
            SaveUserDefinedDict = new Command(new Action(SaveUserDefinedDictAction));
            SaveUserDefinedWord = new Command(new Action(SaveUserDefinedWordAction));
        }

        private bool marked;
        public bool Marked
        {
            get
            {
                return marked;
            }
            set
            {
                SetProperty(ref marked, value);
            }
        }

        private bool waitIndicator;
        public bool WaitIndicator
        {
            get { return waitIndicator; }
            set
            {
                SetProperty(ref waitIndicator, value);
            }
        }

        private bool buttonEnabled;
        public bool ButtonEnabled
        {
            get { return buttonEnabled; }
            set
            {
                SetProperty(ref buttonEnabled, value);
            }
        }

        private void SetWaiting(bool state)
        {
            MainThread.BeginInvokeOnMainThread(()=>
                {
                    WaitIndicator = state;
                    ButtonEnabled = !state;
            }
            );
        }

        private ObservableCollection<int> timeoutSource;
        public ObservableCollection<int> TimeoutSource
        {
            get
            {
                return timeoutSource;
            }
            set
            {
                SetProperty(ref timeoutSource, value);
            }
        }

        private int selectedTimeout;
        public int SelectedTimeout
        {
            get
            {
                return selectedTimeout;
            }
            set
            {
                SetProperty(ref selectedTimeout, value);
            }
        }

        private ObservableCollection<int> multSource;
        public ObservableCollection<int> MultSource
        {
            get
            {
                return multSource;
            }
            set
            {
                SetProperty(ref multSource, value);
            }
        }

        private int selectedMult;
        public int SelectedMult
        {
            get
            {
                return selectedMult;
            }
            set
            {
                SetProperty(ref selectedMult, value);
            }
        }

        private ObservableCollection<int> diffSource;
        public ObservableCollection<int> DiffSource
        {
            get
            {
                return diffSource;
            }
            set
            {
                SetProperty(ref diffSource, value);
            }
        }

        private int selectedDiff;
        public int SelectedDiff
        {
            get
            {
                return selectedDiff;
            }
            set
            {
                SetProperty(ref selectedDiff, value);
            }
        }

        private ObservableCollection<int> sumSource;
        public ObservableCollection<int> SumSource
        {
            get
            {
                return sumSource;
            }
            set
            {
                SetProperty(ref sumSource, value);
            }
        }

        private int selectedSum;
        public int SelectedSum
        {
            get
            {
                return selectedSum;
            }
            set
            {
                SetProperty(ref selectedSum, value);
            }
        }

        public ICommand OpenWebCommand { get; }
        public ICommand SaveSettings { get; }

        public ICommand SaveUserDefinedWord { get; }
        public ICommand SaveUserDefinedDict { get; }
        public ICommand LoadUserDefinedWord { get; }
        public ICommand LoadUserDefinedDict { get; }

        private void SaveUserDefinedWordAction()
        {
            Task t = new Task(async () => { await UpdateUserData("UserWord.xml"); });

            t.RunSynchronously();
        }

        private void SaveUserDefinedDictAction()
        {

            Task t = new Task(async () => { await UpdateUserData("UserDict.xml"); });

            t.RunSynchronously();
                
            
        }

        private async Task UpdateUserData(string fileName)
        {
            try
            {
                SetWaiting(true);

                var resultPrompt = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
                {
                    Message = Language.txtUserDataPrompt,
                    OkText = Language.txtDictOk,
                    CancelText = Language.btnCancel
                });
                if (resultPrompt)
                {

                    var result = await Plugin.FilePicker.CrossFilePicker.Current.PickFile();

                    if (result != null)
                    {
                        XDocument xDocument = XDocument.Parse(Encoding.UTF8.GetString(result.DataArray));
                        IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
                        IFolder destFolder = null;
                        if (!(await PCLHelper.IsFolderExistAsync("UserXML", folder)))
                        {
                            destFolder = await PCLHelper.CreateFolder("UserXML");
                        }
                        else
                        {
                            destFolder = await folder.GetFolderAsync("UserXML");
                        }
                        if (await PCLHelper.IsFileExistAsync(fileName))
                        {
                            await PCLHelper.DeleteFile(fileName);
                        }
                        IFile tFile = await PCLHelper.CreateFile(fileName,destFolder);
                        await tFile.WriteAllTextAsync(xDocument.ToString());
                        SetWaiting(false);
                        UserDialogs.Instance.Alert(Language.txtUserDataUpdated, Language.txtMultOk);
                    }
                    else
                    {
                        SetWaiting(false);
                        UserDialogs.Instance.Alert(Language.txtErrLoadingUserData, Language.txtErrorBtn);
                    }
                }
                else
                {
                    SetWaiting(false);
                }
            }
            catch (Exception ex)
            {
                SetWaiting(false);
                UserDialogs.Instance.Alert(Language.txtErrLoadingUserData+" "+ex.Message, Language.txtErrorTitle);
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        private async Task GetUserData(string fileName)
        {
            try
            {
                SetWaiting(true);
                IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
                if (!(await PCLHelper.IsFolderExistAsync("UserXML",folder)))
                {
                    UserDialogs.Instance.Alert(Language.txtUserDataNoDataUploaded, Language.txtErrorTitle);
                }
                else
                {
                    IFolder destFolder = await folder.GetFolderAsync("UserXML");
                    if(!await PCLHelper.IsFileExistAsync(fileName,destFolder))
                    {
                        UserDialogs.Instance.Alert(Language.txtUserDataNoDataUploaded+" "+fileName, Language.txtErrorTitle);
                    }
                    else
                    {
                        string fileContent = await PCLHelper.ReadAllTextAsync(fileName, destFolder);
                        IFileSave DocLibrary = DependencyService.Get<IFileSave>();
                        bool res = await DocLibrary.SaveXml(fileContent, fileName);
                        if(res)
                        {
                            UserDialogs.Instance.Alert(Language.txtUserDataLoaded + " " + fileName, Language.txtDictOk);
                        }
                        else
                        {
                            UserDialogs.Instance.Alert(Language.txtErrLoadingUserData + " " + fileName, Language.txtDictOk);
                        }
                    }
                }


                SetWaiting(false);
            }
            catch(Exception ex)
            {
                SetWaiting(false);
                UserDialogs.Instance.Alert(Language.txtErrLoadingUserData + " " + ex.Message, Language.txtErrorTitle);
            }
            finally
            {
                await Task.CompletedTask;
            }
        }

        private void LoadUserDefinedWordAction()
        {
            Task t = new Task(async () => { await GetUserData("UserWord.xml"); });

            t.RunSynchronously();
        }

        private void LoadUserDefinedDictAction()
        {
            Task t = new Task(async () => { await GetUserData("UserDict.xml"); });

            t.RunSynchronously();
        }
    }
}