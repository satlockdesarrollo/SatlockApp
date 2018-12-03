namespace SatlockApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class DetailsTripViewModel : BaseViewModel
    {
        private int idViaje;
        private string ultimaLocalizacion;
        private string alias;
        private string sello;
        private string contenedor;
        private string subGrupo;
        private string tipo;
        private string fechaInicial;
        private string horaInicial;
        private string horaFinal;
        private string floc;
        private string uloc;
        private string estado;
        private string motonave;
        private string destino;
        private string latitud;
        private string longitud;
        private string bateria;
        private string tamContenedor;
        private string diasMora;
        private string patio;
        private string lineaNaviera;
        private string motonaven;

        public int IdViaje
        {
            get
            {
                return idViaje;
            }
            set
            {
                SetValue(ref this.idViaje, value);
            }
        }

        public string UltimaLocalizacion
        {
            get
            {
                return ultimaLocalizacion;
            }
            set
            {
                SetValue(ref this.ultimaLocalizacion, value);
            }
        }

        public string Alias
        {
            get
            {
                return alias;
            }
            set
            {
                SetValue(ref this.alias, value);
            }
        }

        public string Sello
        {
            get
            {
                return sello;

            }
            set
            {
                SetValue(ref this.sello, value);
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

        public string SubGrupo
        {
            get
            {
                return subGrupo;

            }
            set
            {
                SetValue(ref this.subGrupo, value);
            }
        }

        public string Tipo
        {
            get
            {
                return tipo;

            }
            set
            {
                SetValue(ref this.tipo, value);
            }
        }

        public string FechaInicial
        {
            get
            {
                return fechaInicial;

            }
            set
            {
                SetValue(ref this.fechaInicial, value);
            }
        }

        public string HoraInicial
        {
            get
            {
                return horaInicial;

            }
            set
            {
                SetValue(ref this.horaInicial, value);
            }
        }

        public string HoraFinal
        {
            get
            {
                return horaFinal;

            }
            set
            {
                SetValue(ref this.horaFinal, value);
            }
        }

        public string Floc
        {
            get
            {
                return floc;

            }
            set
            {
                SetValue(ref this.floc, value);
            }
        }

        public string Uloc
        {
            get
            {
                return uloc;

            }
            set
            {
                SetValue(ref this.uloc, value);
            }
        }

        public string Estado
        {
            get
            {
                return estado;

            }
            set
            {
                SetValue(ref this.estado, value);
            }
        }

        public string Motonave
        {
            get
            {
                return motonave;

            }
            set
            {
                SetValue(ref this.motonave, value);
            }
        }

        public string Destino
        {
            get
            {
                return destino;

            }
            set
            {
                SetValue(ref this.destino, value);
            }
        }

        public string Latitud
        {
            get
            {
                return latitud;

            }
            set
            {
                SetValue(ref this.latitud, value);
            }
        }

        public string Longitud
        {
            get
            {
                return longitud;

            }
            set
            {
                SetValue(ref this.longitud, value);
            }
        }

        public string Bateria
        {
            get
            {
                return bateria;

            }
            set
            {
                SetValue(ref this.bateria, value);
            }
        }

        public string TamContenedor
        {
            get
            {
                return tamContenedor;

            }
            set
            {
                SetValue(ref this.tamContenedor, value);
            }
        }

        public string DiasMora
        {
            get
            {
                return diasMora;

            }
            set
            {
                SetValue(ref this.diasMora, value);
            }
        }

        public string Patio
        {
            get
            {
                return patio;

            }
            set
            {
                SetValue(ref this.patio, value);
            }
        }

        public string LineaNaviera
        {
            get
            {
                return lineaNaviera;

            }
            set
            {
                SetValue(ref this.lineaNaviera, value);
            }
        }

        public string Motonaven
        {
            get
            {
                return motonaven;

            }
            set
            {
                SetValue(ref this.motonaven, value);
            }
        }

        public DetailsTripViewModel(TripItemViewModel trip)
        {
            this.IdViaje = trip.Idviaje;
            this.UltimaLocalizacion = trip.Ultimalocalizacion;
            this.Alias = trip.Alias;
            this.Sello = trip.Sello;
            this.Contenedor = trip.Contenedor;
            this.SubGrupo = trip.Subgrupo;
            this.Tipo = trip.Tipo;
            this.FechaInicial = trip.Fecha_inicial;
            this.HoraInicial = trip.Hora_inicial;
            this.HoraFinal = trip.Hora_final;
            this.Floc = trip.Floc;
            this.Uloc = trip.Uloc;
            this.Estado = trip.Estado;
            this.Motonave = trip.Motonave;
            this.Destino = trip.Destino;
            this.Latitud = trip.Latitud;
            this.Longitud = trip.Longitud;
            this.Bateria = trip.Bateria;
            this.TamContenedor = trip.Tamcontenedor;
            this.DiasMora = trip.Diasmora;
            this.Patio = trip.Patio;
            this.LineaNaviera = trip.Lineanaviera;
            this.Motonaven = trip.Motonaven;
        }


    }
}
