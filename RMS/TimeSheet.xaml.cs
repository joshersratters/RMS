using RMS.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        // Create a new array of project class
        Project[] ProjectArchive = new Project[6];

        // Create a Virtual Server Store
        string[] ProjectVirtualStore = new string[6];
        
        class Project
        {
           public string Name;
           public int[] Hours = new int[6];
        } 
    
        public TimeSheet()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            TextBlockWC.Text = "WC:" + GetTime();
            SetProjects();
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

        //Populate project Virtual Store (acting as remote server)
        public void SetProjects()
        {
            // Populate the .Name field with hard coded Strings for now
            ProjectVirtualStore[0] = "Center Parcs";
            ProjectVirtualStore[1] = "British Land";
            ProjectVirtualStore[2] = "Healthcare at Home";
            ProjectVirtualStore[3] = "Volkswagen";
            ProjectVirtualStore[4] = "Merlin Entertainment";
            ProjectVirtualStore[5] = "Carbon Trust";

            // Create an index reference

            for (int index = 0; index <= ProjectVirtualStore.Length; index++)
            {
                ProjectArchive[index].Name = ProjectVirtualStore[index];
            }            
        }

        //Initialise TimeSheet UI elements
        public void InitialiseUI()
        {
            //Sets combo box content (hard coded x6 currently)
            ComboBoxProject1.Content = "1. " + ProjectArchive[0].Name;
            ComboBoxProject2.Content = "2. " + ProjectArchive[1].Name;
            ComboBoxProject3.Content = "3. " + ProjectArchive[2].Name;
            ComboBoxProject4.Content = "4. " + ProjectArchive[3].Name;
            ComboBoxProject5.Content = "5. " + ProjectArchive[4].Name;
            ComboBoxProject6.Content = "6. " + ProjectArchive[5].Name;
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
        }

        //Gets the current selected project
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
