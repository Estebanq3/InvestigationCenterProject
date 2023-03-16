using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentAssertions;
using ResearchRepository.Application.Authentication;
using WebApplication_ResearchRepository;

namespace IntegrationTests.Infrastructure.Authentication.Repositories
{
    public class AuthenticationRepositoryIntegrationTests :
    IClassFixture<AuthenticationFactory<Startup>>
    {
        private readonly AuthenticationFactory<Startup> _factory;
        public
        AuthenticationRepositoryIntegrationTests(AuthenticationFactory<Startup>
        factory)
        {
            _factory = factory;
        }

        //Users available in database for tests:
        //Email/Username                    Password
        //-----------------------------------------------
        //sebastian.monterocastro@ucr.ac.cr C0ntr@sena1
        //andrea.alvaradoacon@ucr.ac.cr     C0ntr@sena1
        //greivin.sanchezgarita@ucr.ac.cr   C0ntr@sena1

        [Fact]
        public async Task GetUserByTheirEmail()
        {
            var repository = _factory.Server.Services.GetRequiredService<IAuthenticationService>();
            var user = repository.getUserByEmail("sebastian.monterocastro@ucr.ac.cr");
            user.Should().NotBeNull();
        }

        [Fact]
        public async Task IncorrectPasswordInserted()
        {
            string userEmail = "sebastian.monterocastro@ucr.ac.cr";
            string invalidPassword = "contra";
            var repository = _factory.Server.Services.GetRequiredService<IAuthenticationService>();
            var user = await repository.getUserByEmail(userEmail);
            bool passwordIsCorrect = await repository.passwordIsValid(user,invalidPassword);
            passwordIsCorrect.Should().BeFalse();
        }

        [Fact]
        public async Task PasswordIsCorrect() {
            string userEmail = "sebastian.monterocastro@ucr.ac.cr";
            string invalidPassword = "C0ntr@sena1";
            var repository = _factory.Server.Services.GetRequiredService<IAuthenticationService>();
            var user = await repository.getUserByEmail(userEmail);
            bool passwordIsCorrect = await repository.passwordIsValid(user, invalidPassword);
            passwordIsCorrect.Should().BeTrue();
        }

        [Fact]
        public async Task ChangePasswordOfUser() {
            string userEmail = "andrea.alvaradoacon@ucr.ac.cr";
            string oldPassword = "C0ntr@sena1";
            string newPassword = "newC0ntr@sena";
            var repository = _factory.Server.Services.GetRequiredService<IAuthenticationService>();
            var user = await repository.getUserByEmail(userEmail);
            bool changedPassword = await repository.ChangePassword(user,oldPassword,newPassword);
            await repository.ChangePassword(user, newPassword, oldPassword);
            changedPassword.Should().BeTrue();
        }
        [Fact]
        public async Task EncryptionSystemTest() {
            string testString = "This string should be encrypted and decrypted succesfully.";
            string encryptionKey = "testKey";
            var repository = _factory.Server.Services.GetRequiredService<IAuthenticationService>();
            string encryptedString = repository.EncryptString(testString,encryptionKey);
            string decryptedString = repository.Decrypt(encryptedString, encryptionKey);

            decryptedString.Should().BeEquivalentTo(testString);
        }

        [Fact]
        public async Task CheckIfUserIsConfirmed() {
            string userEmail = "andrea.alvaradoacon@ucr.ac.cr";
            var repository = _factory.Server.Services.GetRequiredService<IAuthenticationService>();
            bool isConfirmed = await repository.isConfirmed(userEmail);
            isConfirmed.Should().BeTrue();
        }

        /*
         * ESTE TEST FUNCIONA LA PRIMERA VEZ CUANDO ESTAN FRESCOS LOS DATOS PERO
         * SI SE CORRE DESPUES NO SIRVE PORQUE LA CUENTA YA ESTA CONFIRMADA
         * HAY QUE ENCONTRAR UNA MANERA DE BORRAR O DES CONFIRMAR ESTA CUENTA
         * 
        [Fact]
        public async Task ConfirmAccountThatIsNotConfirmed() {
            string notConfirmedEmail = "greivin.sanchezgarita@ucr.ac.cr";
            bool userIsConfirmed = false;
            var repository = _factory.Server.Services.GetRequiredService<IAuthenticationService>();
            if (!await repository.isConfirmed(notConfirmedEmail)) {
                userIsConfirmed = await repository.confirmAccount(notConfirmedEmail);
            }
            userIsConfirmed.Should().BeTrue();
        }
        */
        [Fact]
        public async Task TryToRegisterAnAlreadyExistingEmail() {
            string alreadyRegistered = "sebastian.monterocastro@ucr.ac.cr";
            var repository = _factory.Server.Services.GetRequiredService<IAuthenticationService>();
            bool isAlreadyRegistered = await repository.emailIsAlreadyRegistered(alreadyRegistered);
            isAlreadyRegistered.Should().BeTrue();
        }
    }
}