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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimeSheet : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        //Declare objects
        int? SelectIndex = null;
        int ProjectCount = 20;
        public string[] ProjectName = new string[6];
        int[] P1Day = new int[7];
        int[] P2Day = new int[7];
        int[] P3Day = new int[7];
        int[] P4Day = new int[7];
        int[] P5Day = new int[7];
        int[] P6Day = new int[7];
        int[] P1Hours = new int[7];
        int[] P2Hours = new int[7];
        int[] P3Hours = new int[7];
        int[] P4Hours = new int[7];
        int[] P5Hours = new int[7];
        int[] P6Hours = new int[7];
        string CurentDate = DateTime.Today.ToString();
        
        class Project
        {
           public string name;
           public int hours;
           public int index;

        } 
    
        public TimeSheet()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            TextBlockWC.Text = "WC:" + CurentDate;
            SetProjects();
            InitialiseUI();
        }

        //Sets the Projects Variables
        public void SetProjects()
        {
            string[] ProjectArchive = new string[ProjectCount];
            ProjectArchive[0] = "Center Parcs";
            ProjectArchive[1] = "British Land";
            ProjectArchive[2] = "Healthcare at Home";
            ProjectArchive[3] = "Volkswagen";
            ProjectArchive[4] = "Merlin Entertainment";
            ProjectArchive[5] = "Carbon Trust";

            int index = 0;

            while (index < ProjectName.Length)
            {
                ProjectName[index] = ProjectArchive[index];
                index++;
            }            
        }

        //Initialise TimeSheet UI elements
        public void InitialiseUI()
        {
            ComboBoxProject1.Content = "1. " + ProjectName[0];
            ComboBoxProject2.Content = "2. " + ProjectName[1];
            ComboBoxProject3.Content = "3. " + ProjectName[2];
            ComboBoxProject4.Content = "4. " + ProjectName[3];
            ComboBoxProject5.Content = "5. " + ProjectName[4];
            ComboBoxProject6.Content = "6. " + ProjectName[5];
        }

        //Gets the current selected project
        public int SelectProject(int ProjectNumber)
        {
            var index = 0;

            switch (ProjectNumber)
            {
                case 0:
                    index = 1;
                    break;

                case 1:
                    index = 2;
                    break;

                case 2:
                    index = 3;
                    break;

                case 3:
                    index = 4;
                    break;

                case 4:
                    index = 5;
                    break;

                case 5:
                    index = 6;
                    break;

                default:
                    index = 0;                  
                    break;
            }

            return index;
        } 

        // Submits the timesheet variables to correct project
        public void SubmitTimesheet(int ProjectNumber)
        {

            switch (ProjectNumber)
            {
                case 1:
                    P1Day[0] = (int.Parse(TextBoxProjectMon.Text));
                    P1Day[1] = (int.Parse(TextBoxProjectTue.Text));
                    P1Day[2] = (int.Parse(TextBoxProjectWed.Text));
                    P1Day[3] = (int.Parse(TextBoxProjectThu.Text));
                    P1Day[4] = (int.Parse(TextBoxProjectFri.Text));
                    P1Day[5] = (int.Parse(TextBoxProjectSat.Text));
                    P1Day[6] = (int.Parse(TextBoxProjectSun.Text));
                    break;

                case 2:
                    P2Day[0] = (int.Parse(TextBoxProjectMon.Text));
                    P2Day[1] = (int.Parse(TextBoxProjectTue.Text));
                    P2Day[2] = (int.Parse(TextBoxProjectWed.Text));
                    P2Day[3] = (int.Parse(TextBoxProjectThu.Text));
                    P2Day[4] = (int.Parse(TextBoxProjectFri.Text));
                    P2Day[5] = (int.Parse(TextBoxProjectSat.Text));
                    P2Day[6] = (int.Parse(TextBoxProjectSun.Text));
                    break;

                case 3:
                    P3Day[0] = (int.Parse(TextBoxProjectMon.Text));
                    P3Day[1] = (int.Parse(TextBoxProjectTue.Text));
                    P3Day[2] = (int.Parse(TextBoxProjectWed.Text));
                    P3Day[3] = (int.Parse(TextBoxProjectThu.Text));
                    P3Day[4] = (int.Parse(TextBoxProjectFri.Text));
                    P3Day[5] = (int.Parse(TextBoxProjectSat.Text));
                    P3Day[6] = (int.Parse(TextBoxProjectSun.Text));
                    break;

                case 4:
                    P4Day[0] = (int.Parse(TextBoxProjectMon.Text));
                    P4Day[1] = (int.Parse(TextBoxProjectTue.Text));
                    P4Day[2] = (int.Parse(TextBoxProjectWed.Text));
                    P4Day[3] = (int.Parse(TextBoxProjectThu.Text));
                    P4Day[4] = (int.Parse(TextBoxProjectFri.Text));
                    P4Day[5] = (int.Parse(TextBoxProjectSat.Text));
                    P4Day[6] = (int.Parse(TextBoxProjectSun.Text));
                    break;

                case 5:
                    P5Day[0] = (int.Parse(TextBoxProjectMon.Text));
                    P5Day[1] = (int.Parse(TextBoxProjectTue.Text));
                    P5Day[2] = (int.Parse(TextBoxProjectWed.Text));
                    P5Day[3] = (int.Parse(TextBoxProjectThu.Text));
                    P5Day[4] = (int.Parse(TextBoxProjectFri.Text));
                    P5Day[5] = (int.Parse(TextBoxProjectSat.Text));
                    P5Day[6] = (int.Parse(TextBoxProjectSun.Text));
                    break;

                case 6:
                    P6Day[0] = (int.Parse(TextBoxProjectMon.Text));
                    P6Day[1] = (int.Parse(TextBoxProjectTue.Text));
                    P6Day[2] = (int.Parse(TextBoxProjectWed.Text));
                    P6Day[3] = (int.Parse(TextBoxProjectThu.Text));
                    P6Day[4] = (int.Parse(TextBoxProjectFri.Text));
                    P6Day[5] = (int.Parse(TextBoxProjectSat.Text));
                    P6Day[6] = (int.Parse(TextBoxProjectSun.Text));
                    break;

                case 0:
                default:
                    break;
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
            if (SelectIndex != null)
            {
                SubmitTimesheet(SelectIndex.Value);
            }

            else
            {
                MessageDialog msgbox = new MessageDialog("Please select a project");
                await msgbox.ShowAsync();
            }
           
        }

        private void ProjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int CurrentProject = ProjectComboBox.SelectedIndex;
            SelectIndex = SelectProject(CurrentProject);
        }
    }
}
