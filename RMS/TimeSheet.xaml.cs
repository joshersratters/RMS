using RMS.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace RMS
{
    public sealed partial class TimeSheet : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        //Use the ApplicationData.LocalFolder property to get the files in a StorageFolder object.
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        // Create a new list of project class
        List<Project> ProjectArchive = new List<Project>();

        // Create a Virtual Server Store
        List<string> ProjectVirtualStore = new List<string>();

        // Create list of combobox items to use as archive
        List<ComboBoxItem> ComboBoxArchive = new List<ComboBoxItem>();

        // Create list of feedbackMessage class
        List<FeedbackMessage> FeedbackMessageArchive = new List<FeedbackMessage>();

        class FeedbackMessage
        {
            public string ErrorMessage;
            public bool Successful;
            public int ErrorID;

            // Contructor
            public FeedbackMessage(string ErrorMesage, Boolean Successful, int ErrorID)
            {
                this.ErrorMessage = ErrorMesage;
                this.Successful = Successful;
                this.ErrorID = ErrorID;
            }

        }
        
        class Project
        {
           public string Name;
           public int[] Hours = new int[7]; // 7 elements for each day of week Mon - Sun
            
           // Constructor
           public Project(string Name)
           {
                this.Name = Name;
           }
        }

        public TimeSheet()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
           // ReadData();
            SetProjects();
            SetComboBoxItem();
            InitialiseUI();
        }

        public string GetTime()
        {
            DateTime DateAndTime = DateTime.Now;
            string year = DateAndTime.Year.ToString();
            string month = DateAndTime.Month.ToString();
            string day = DateAndTime.Day.ToString();
            string DateOnly = day + "/" + "0" + month + "/" + year;

            return DateOnly;
        }

        // Populates ComboBox with Items
        public void SetComboBoxItem()
        {
            for (int i = 0; i < ProjectArchive.Count; i++)
            {
                ComboBoxArchive.Add(new ComboBoxItem());
                ComboBoxArchive[i].Content = ProjectArchive[i].Name;
            }

        }

        // Populate project Virtual Store (acting as remote server)
        public void SetProjects()
        {
            ProjectArchive.Add(new Project("Center Parcs"));
            ProjectArchive.Add(new Project("British Land"));
            ProjectArchive.Add(new Project("Healthcare at Home"));
            ProjectArchive.Add(new Project("Volkswagen"));
            ProjectArchive.Add(new Project("Merlin Entertainment"));
            ProjectArchive.Add(new Project("Carbon Trust"));       
        }

        // Initialise TimeSheet UI elements
        public void InitialiseUI()
        {
            // Set textblock time
            TextBlockWC.Text = GetTime();

            // Sets combo box content (dynamic)
            ProjectComboBox.ItemsSource = ComboBoxArchive;
        }

        //Update TimeSheet UI elements
        public void UpdateUI()
        {
            //Sets textbox content
            TextBoxProjectMon.Text = ProjectArchive[ProjectComboBox.SelectedIndex].Hours[0].ToString(); // Monday
            TextBoxProjectTue.Text = ProjectArchive[ProjectComboBox.SelectedIndex].Hours[1].ToString(); // Tuesday
            TextBoxProjectWed.Text = ProjectArchive[ProjectComboBox.SelectedIndex].Hours[2].ToString(); // Wednesday
            TextBoxProjectThu.Text = ProjectArchive[ProjectComboBox.SelectedIndex].Hours[3].ToString(); // Thursday
            TextBoxProjectFri.Text = ProjectArchive[ProjectComboBox.SelectedIndex].Hours[4].ToString(); // Friday
            TextBoxProjectSat.Text = ProjectArchive[ProjectComboBox.SelectedIndex].Hours[5].ToString(); // Saturday
            TextBoxProjectSun.Text = ProjectArchive[ProjectComboBox.SelectedIndex].Hours[6].ToString(); // Sunday

            TextBlockStatusFeedback.Text = "Unknown";
            TextBlockStatusFeedback.Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(0xFF, 0xF9, 0xB6, 0x02));
        }

        // Gets the current selected project
        public int SelectProject(int ProjectNumber)
        {
            int? index;

            switch (ProjectNumber)
            {
                case 0:
                    index = 0;
                    break;

                case 1:
                    index = 1;
                    break;

                case 2:
                    index = 2;
                    break;

                case 3:
                    index = 3;
                    break;

                case 4:
                    index = 4;
                    break;

                case 5:
                    index = 5;
                    break;

                default:
                    index = 6;
                    break;
            }

            return index.Value;
        } 

        // Submits the timesheet variables to correct project
        public void UpdateHours(int ProjectNumber)
        {
            ProjectArchive[ProjectNumber].Hours[0] = (int.Parse(TextBoxProjectMon.Text)); // Monday
            ProjectArchive[ProjectNumber].Hours[1] = (int.Parse(TextBoxProjectTue.Text)); // Tuesday
            ProjectArchive[ProjectNumber].Hours[2] = (int.Parse(TextBoxProjectWed.Text)); // Wednesday
            ProjectArchive[ProjectNumber].Hours[3] = (int.Parse(TextBoxProjectThu.Text)); // Thursday
            ProjectArchive[ProjectNumber].Hours[4] = (int.Parse(TextBoxProjectFri.Text)); // Friday
            ProjectArchive[ProjectNumber].Hours[5] = (int.Parse(TextBoxProjectSat.Text)); // Saturday
            ProjectArchive[ProjectNumber].Hours[6] = (int.Parse(TextBoxProjectSun.Text)); // Sunday                  
        }

        public async void PersistData()
        {
            // Write data to a file
            StorageFile ProjectPersistentStore = await localFolder.CreateFileAsync("projectDataFile.txt", CreationCollisionOption.ReplaceExisting);

            for (int i = 0; i < ProjectArchive.Count; i++)
            {
                await FileIO.WriteTextAsync(ProjectPersistentStore, ProjectArchive[i].Hours[0].ToString()); // Monday
                await FileIO.WriteTextAsync(ProjectPersistentStore, ProjectArchive[i].Hours[1].ToString()); // Tuesday
                await FileIO.WriteTextAsync(ProjectPersistentStore, ProjectArchive[i].Hours[2].ToString()); // Wednesday
                await FileIO.WriteTextAsync(ProjectPersistentStore, ProjectArchive[i].Hours[3].ToString()); // Thursday
                await FileIO.WriteTextAsync(ProjectPersistentStore, ProjectArchive[i].Hours[4].ToString()); // Friday
                await FileIO.WriteTextAsync(ProjectPersistentStore, ProjectArchive[i].Hours[5].ToString()); // Saturday
                await FileIO.WriteTextAsync(ProjectPersistentStore, ProjectArchive[i].Hours[6].ToString()); // Sunday
            }

            if (ProjectArchive.Count != 0)
            {
                FeedbackMessageArchive.Add(new FeedbackMessage("Successful", true, 0));
                TextBlockStatusFeedback.Text = FeedbackMessageArchive.First().ErrorMessage;
                TextBlockStatusFeedback.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
            }
            else
            {
                TextBlockStatusFeedback.Text = "Not Submitted";
            }
        }

        public async void ReadData()
        {
            try
            {
                StorageFile ProjectPersistentStore = await localFolder.GetFileAsync("projectDataFile.txt");
                String ProjectHours = await FileIO.ReadTextAsync(ProjectPersistentStore);
            }
            catch (Exception)
            {
                MessageDialog msgbox = new MessageDialog("Failed to load saved data.");
                await msgbox.ShowAsync();
                throw;
            }
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void ButtonSubmit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ProjectComboBox.SelectedIndex >= 0)
            {
                UpdateHours(ProjectComboBox.SelectedIndex);
                PersistData();
                UpdateUI();
            }

            else
            {
                MessageDialog msgbox = new MessageDialog("Please select a project");
                await msgbox.ShowAsync();
            }
           
        }

        private void ProjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
        }
    }
}
