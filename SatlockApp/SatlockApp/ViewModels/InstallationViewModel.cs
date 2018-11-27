namespace SatlockApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class InstallationViewModel : BaseViewModel
    {

        private string unidad;
        private string contenedor;
        private string tamano;
        private string control;
        private string embarcacion;
        private string maxDate;
        private string minDate;
        private string today;
        private bool isLoading;
        private bool installationEnabled;

        public string Unidad
        {
            get
            {
                return unidad;
            }
            set
            {
                SetValue(ref this.unidad, value);
            }
        }

        public string Contenedor
        {
            get
            {
                return contenedor;
            }
            set
            {
                SetValue(ref this.contenedor, value);
            }
        }

        public string Tamano
        {
            get
            {
                return tamano;
            }
            set
            {
                SetValue(ref this.tamano, value);
            }
        }


        public string Control
        {
            get
            {
                return control;
            }
            set
            {
                SetValue(ref this.control, value);
            }
        }

        public string Embarcacion
        {
            get
            {
                return embarcacion;
            }
            set
            {
                SetValue(ref this.embarcacion, value);
            }
        }

        public string MaxDate
        {

            get
            {
                return maxDate;
            }
            set
            {
                SetValue(ref this.maxDate, value);
            }
        }

        public string MinDate
        {

            get
            {
                return minDate;
            }
            set
            {
                SetValue(ref this.minDate, value);
            }
        }

        public string Today
        {

            get
            {
                return today;
            }
            set
            {
                SetValue(ref this.today, value);
            }
        }

        public bool InstallationEnabled
        {

            get
            {
                return installationEnabled;
            }
            set
            {
                SetValue(ref this.installationEnabled, value);
            }
        }

        public bool IsLoading
        {

            get
            {
                return isLoading;
            }
            set
            {
                SetValue(ref this.isLoading, value);
            }
        }

        public InstallationViewModel()
        {
            this.updateDates();
            this.InstallationEnabled = true;
            this.IsLoading = false;

            var mainViewModel = MainViewModel.GetInstance();
            this.Unidad = mainViewModel.MobileImei;
        }

        private void updateDates()
        {

            var now = DateTime.Now;
            this.Today = now.ToString("MM/dd/yyyy");

            var max = now.AddMonths(6);
            this.MaxDate = max.ToString("MM/dd/yyyy");

            var min = now.AddDays(-1);
            this.MinDate = min.ToString("MM/dd/yyyy");

        }

    }
}
