using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using AppointmentControl.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace AppointmentControl.Data
{
    public class DoctorManager
    {
        // Azure
        private readonly IMobileServiceTable<Doctor> _table;

        public DoctorManager()
        {
            var azureClient = new MobileServiceClient(
                Constants.AzureApplicationUrl,
                Constants.AzureApplicationKey);

            _table = azureClient.GetTable<Doctor>();
        }

        public async Task<Doctor> GetTaskAsync(string id)
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

        public async Task<ObservableCollection<Doctor>> GetUsersAsync()
        {
            try
            {
                return new ObservableCollection<Doctor>(
                    await _table.Where(doctor => doctor.Id != null).ToListAsync());
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

        public async Task SaveTaskAsync(Doctor doctor)
        {
            if (doctor.Id == null)
            {
                await _table.InsertAsync(doctor);
            }
            else
            {
                await _table.UpdateAsync(doctor);
            }
        }
    }
}