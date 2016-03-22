using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AppointmentControl.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace AppointmentControl.Data
{
    using System.Collections.Generic;

    public class PatientManager
    {
        // Azure
        private static IMobileServiceTable<Patient> _table;

        public PatientManager()
        {
            var azureClient = new MobileServiceClient(
                Constants.AzureApplicationUrl,
                Constants.AzureApplicationKey);

            _table = azureClient.GetTable<Patient>();
        }

        public async Task<Patient> GetTaskAsync(string id)
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

        public async Task<ObservableCollection<Patient>> GetUsersAsync()
        {
            try
            {
                return new ObservableCollection<Patient>(
                    await _table.Where(patient => patient.Id != null).ToListAsync());
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

        public async Task<List<Patient>> GetPatientsAsync()
        {
            try
            {
                return new List<Patient>(
                    await _table.Where(patient => patient.Id != null).ToListAsync());
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

        public async Task SaveTaskAsync(Patient patient)
        {
            if (patient.Id == null)
            {
                await _table.InsertAsync(patient);
            }
            else
            {
                await _table.UpdateAsync(patient);
            }
        }
    }
}