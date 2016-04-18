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
        int ProjectIndex;
        string ProjectNameOne = "";
        string ProjectNameTwo = "";
        string ProjectNameThree = "";
        string ProjectNameFour = "";
        string ProjectNameFive = "";
        string ProjectNameSix = "";
        int[] Day = new int[7];
        int[] Hours = new int[7];
        string CurentDate = DateTime.Today.ToString(); 
    
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

        // Sets the Projects Variables
        public void SetProjects()
        {
            ProjectNameOne = "Center Parcs";
            ProjectNameTwo = "British Land";
            ProjectNameThree = "Healthcare";
            ProjectNameFour = "Project Four";
            ProjectNameFive = "Project Five";
            ProjectNameSix = "Project Six";
        }

        //Initialise TimeSheet UI elements
        public void InitialiseUI()
        {
            ComboBoxProject1.Content = "1. " + ProjectNameOne;
            ComboBoxProject2.Content = "2. " + ProjectNameTwo;
            ComboBoxProject3.Content = "3. " + ProjectNameThree;
            ComboBoxProject4.Content = "4. " + ProjectNameFour;
            ComboBoxProject5.Content = "5. " + ProjectNameFive;
            ComboBoxProject6.Content = "6. " + ProjectNameSix;
        }

        //Gets the current selected project
        public int SelectProject(int ProjectNumber)
        {
            var index = -1;

            switch (ProjectNumber)
            {
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

                case 6:
                    index = 6;
                    break;

                case 0:
                default:
                    index = 0;                  
                    break;
            }

            return index;
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

        private void ButtonSubmit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Hours[0] = (int.Parse(TextBoxProject1Mon.Text));
        }

        private void ProjectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int CurrentProject = ProjectComboBox.SelectedIndex;
            ProjectIndex = SelectProject(CurrentProject);
        }
    }
}
