// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Scio_Consulting">
//   Copyright ©  Scio_Consulting. Todos los derechos estan reservados.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AppointmentControl.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The user.
    /// </summary>
    public class User
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the speciality.
        /// </summary>
        [JsonProperty(PropertyName = "speciality")]
        public string Speciality { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        [JsonProperty(PropertyName = "zip")]
        public string Zip { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether isdoctor.
        /// </summary>
        [JsonProperty(PropertyName = "isdoctor")]
        public bool isdoctor { get; set; }

        #endregion
    }
}