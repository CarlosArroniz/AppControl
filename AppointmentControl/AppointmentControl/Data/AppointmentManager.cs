using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AppointmentControl.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace AppointmentControl.Data
{
    public class AppointmentManager
    {
        // Azure
        private readonly IMobileServiceTable<Appointment> _table;

        public AppointmentManager()
        {
            var azureClient = new MobileServiceClient(
                Constants.AzureApplicationUrl,
                Constants.AzureApplicationKey);

            _table = azureClient.GetTable<Appointment>();
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
                    await _table.OrderBy(appointment => appointment.Id).ToListAsync());
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

        public async Task<ObservableCollection<Appointment>> GetAppointmentsAsync(string doctorId, string patientId)
        {
            try
            {
                return new ObservableCollection<Appointment>(
                    await _table.Where(appointment => (appointment.DoctorId==doctorId) && (appointment.PatientId==patientId)).ToListAsync());
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

        public async Task<ObservableCollection<Appointment>> GetAppointmentsAsync(Doctor doctor)
        {
            try
            {
                return new ObservableCollection<Appointment>(
                    await _table.Where(appointment => appointment.DoctorId == doctor.Id).ToListAsync());
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

        public async Task<ObservableCollection<Appointment>> GetAppointmentsAsync(Patient patient)
        {
            try
            {
                return new ObservableCollection<Appointment>(
                    await _table.Where(appointment => appointment.PatientId == patient.Id).ToListAsync());
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
    }
}