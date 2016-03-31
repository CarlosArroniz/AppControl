using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AppointmentControl.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace AppointmentControl.Data
{
    public class AppointmentManager
    {
        // Azure
        private readonly IMobileServiceTable<Appointment> _table;
        private static readonly Lazy<AppointmentManager> instance = new Lazy<AppointmentManager>(() => new AppointmentManager());

        private AppointmentManager()
        {
            var azureClient = new MobileServiceClient(
                Constants.AzureApplicationUrl,
                Constants.AzureApplicationKey);

            _table = azureClient.GetTable<Appointment>();
        }

        public static AppointmentManager Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public async Task<Appointment> GetTaskAsync(string id)
        {
            try
            {
                return await _table.LookupAsync(id);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;
        }

        public async Task<ObservableCollection<Appointment>> GetAppointmentsAsync()
        {
            try
            {
                return new ObservableCollection<Appointment>(
                    await _table.Where(appointment => appointment.DoctorId != null && appointment.PatientId != null).ToListAsync());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return new ObservableCollection<Appointment>();
        }

        public async Task<ObservableCollection<Appointment>> GetAppointmentsAsync(string userId)
        {
            try
            {
                return new ObservableCollection<Appointment>(
                    await _table.Where(appointment => (appointment.DoctorId == userId) || (appointment.PatientId == userId)).ToListAsync());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return new ObservableCollection<Appointment>();
        }

        public async Task<ObservableCollection<Appointment>> GetAppointmentsOfDoctorAsync(string doctorId)
        {
            try
            {
                return new ObservableCollection<Appointment>(
                    await _table.Where(appointment => appointment.DoctorId == doctorId).ToListAsync());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return new ObservableCollection<Appointment>();
        }

        public async Task<ObservableCollection<Appointment>> GetAppointmentsOfPatientAsync(string patientId)
        {
            try
            {
                return new ObservableCollection<Appointment>(
                    await _table.Where(appointment => appointment.PatientId == patientId).ToListAsync());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return new ObservableCollection<Appointment>();
        }

        public async Task SaveTaskAsync(Appointment appointment)
        {
            if (appointment.Id == null)
            {
                await _table.InsertAsync(appointment);
            }
            else
            {
                await _table.UpdateAsync(appointment);
            }
        }

        public async Task<ObservableCollection<Appointment>> GetAppointmentsOfDoctorAsync(string doctorId, DateTime appointmentStart)
        {
            var appointments = await GetAppointmentsOfDoctorAsync(doctorId);
            ObservableCollection<Appointment> result = new ObservableCollection<Appointment>();
            foreach (var appointment in appointments)
            {
                var start = DateTime.Parse(appointment.StartDate, new DateTimeFormatInfo());
                if (start >= appointmentStart)
                {
                    result.Add(appointment);
                }
            }

            return result;
        }
    }
}