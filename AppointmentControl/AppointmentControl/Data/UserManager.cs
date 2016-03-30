using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppointmentControl.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace AppointmentControl.Data
{
    public class UserManager
    {
        // Azure
        private readonly IMobileServiceTable<User> _table;

        private static readonly Lazy<UserManager> instance = new Lazy<UserManager>(() => new UserManager());

        private UserManager()
        {
            var azureClient = new MobileServiceClient(
                Constants.AzureApplicationUrl,
                Constants.AzureApplicationKey);

            _table = azureClient.GetTable<User>();
        }

        public static UserManager Instance
        {
            get
            {
                return instance.Value;
            }
        }

        public async Task<User> GetTaskAsync(string id)
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

        public async Task<ObservableCollection<User>> GetUsersAsync()
        {
            try
            {
                return new ObservableCollection<User>(
                    await _table.Where(user => user.Username != null).ToListAsync());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return new ObservableCollection<User>();
        }

        public async Task<ObservableCollection<User>> GetPatientsAsync()
        {
            try
            {
                return new ObservableCollection<User>(
                    await _table.Where(user => user.Username != null && user.isdoctor == false).ToListAsync());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return new ObservableCollection<User>();
        }

        public async Task<ObservableCollection<User>> GetDoctorsAsync()
        {
            try
            {
                return new ObservableCollection<User>(
                    await _table.Where(user => user.isdoctor).ToListAsync());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return new ObservableCollection<User>();
        }

        public async Task SaveTaskAsync(User user)
        {
            if (user.Id == null)
            {
                await _table.InsertAsync(user);
            }
            else
            {
                await _table.UpdateAsync(user);
            }
        }

        public async Task<User> FindUser(User user)
        {
            var userList = await _table.Where(u => u.Username == user.Username).ToListAsync();
            return userList.First();
        }

        public async Task<User> FindUser(string username)
        {
            try
            {
                var userPassList =
                    await _table.Where(p => p.Username == username).ToListAsync();
                return userPassList.First();
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
        
        public async Task<User> FindUsernameAndPass(string user, string pass)
        {
            try
            {
                var userPassList =
                    await _table.Where(p => p.Username == user && p.Password == pass).ToListAsync();
                return userPassList.First();
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

        public async Task<ObservableCollection<User>> GetFiltersAsync(string city, string speciality)
        {
            try
            {
                return new ObservableCollection<User>(
                    await _table.Where(user => user.City == city && user.Speciality == speciality).ToListAsync());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return new ObservableCollection<User>();
        }

        public async Task<ObservableCollection<User>> GetUsersByCityAsync(string city)
        {
            try
            {
                return new ObservableCollection<User>(
                    await _table.Where(user => user.City == city && user.isdoctor).ToListAsync());
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return new ObservableCollection<User>();
        }

    }
}