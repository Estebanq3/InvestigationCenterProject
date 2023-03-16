using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using FluentAssertions;
using ResearchRepository.Application.Authorization;
using WebApplication_ResearchRepository;

namespace IntegrationTests.Infrastructure.Authorization.Repositories
{
    public class AuthorizationRepositoryIntegrationTests :
    IClassFixture<AuthorizationFactory<Startup>>
    {
        private readonly AuthorizationFactory<Startup> _factory;
        private static string[] roleNames = { "Administrador", "Administrador de Centro", "Administrador de Grupo", "Colaborador de Centro", "Colaborador de Grupo" };
        public
        AuthorizationRepositoryIntegrationTests(AuthorizationFactory<Startup>
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

        public async Task RolesAreConfiguredSuccesfully() {
            var repository = _factory.Server.Services.GetRequiredService<IAuthorizationServices>();
            await repository.configureRoles();
            int rolesInDatabase = (await repository.getRoles()).Count();
            rolesInDatabase.Should().Be(roleNames.Count());
        }

        public async Task AssignRoleToExistingUser() {
            var repository = _factory.Server.Services.GetRequiredService<IAuthorizationServices>();
            string userEmail = "sebastian.monterocastro@ucr.ac.cr";
            string roleToAssign = "Administrador";
            await repository.assignRole(userEmail, roleToAssign);
            bool isPartOfRole = await repository.isPartOfRole(userEmail, roleToAssign);
            await repository.removeRole(userEmail, roleToAssign);
            isPartOfRole.Should().BeTrue();
        }

        public async Task RemoveASpecificRoleFromUser() {
            var repository = _factory.Server.Services.GetRequiredService<IAuthorizationServices>();
            string userEmail = "sebastian.monterocastro@ucr.ac.cr";
            //Se añaden roles de prueba
            await repository.assignRole(userEmail, "Administrador");
            await repository.assignRole(userEmail, "Administrador de Centro");
            await repository.assignRole(userEmail, "Administrador de Grupo");
            
            //Se quita el rol de Administrador De Centro
            await repository.removeRole(userEmail, "Administrador de Centro");
            var userRoles = await repository.getUserRoles(userEmail);
            userRoles.Count().Should().Be(2);
        }

        public async Task SwitchARoleFromUser() {
            var repository = _factory.Server.Services.GetRequiredService<IAuthorizationServices>();
            string userEmail = "andrea.alvaradoacon@ucr.ac.cr";
            string roleThatUserIsPartOf = "Administrador";
            string roleThatUserIsNotPartOf = "Colaborador de Centro";
            await repository.assignRole(userEmail, roleThatUserIsPartOf);
            await repository.switchRoleState(userEmail, roleThatUserIsPartOf, true);
            await repository.switchRoleState(userEmail, roleThatUserIsNotPartOf, false);

            bool wasPartOfAndNotAnymore = await repository.isPartOfRole(userEmail,roleThatUserIsPartOf);
            bool wasNotPartOfAndNowIs = await repository.isPartOfRole(userEmail, roleThatUserIsNotPartOf);

            wasPartOfAndNotAnymore.Should().BeFalse();
            wasNotPartOfAndNowIs.Should().BeTrue();
        }

        public async Task VerifyGetAllUsers() {
            var repository = _factory.Server.Services.GetRequiredService<IAuthorizationServices>();
            var allUsers = await repository.getUsers();
            allUsers.Count().Should().Be(3);
        }

        public async Task UserIsNotPartOfRole() {
            var repository = _factory.Server.Services.GetRequiredService<IAuthorizationServices>();
            string userWithNoRoles = "greivin.sanchezgarita@ucr.ac.cr";
            bool isAdministrator = await repository.isPartOfRole(userWithNoRoles, "Administrador");
            isAdministrator.Should().BeFalse();
        }

        public async Task VerifyGetAllRoles() {
            var repository = _factory.Server.Services.GetRequiredService<IAuthorizationServices>();
            var allRoles = await repository.getRoles();
            allRoles.Count().Should().Be(roleNames.Count());
        }

        public async Task VerifyRoleCountFromUser()
        {
            var repository = _factory.Server.Services.GetRequiredService<IAuthorizationServices>();
            string userEmail = "sebastian.monterocastro@ucr.ac.cr";
            //Se añaden roles de prueba
            await repository.assignRole(userEmail, "Administrador");
            await repository.assignRole(userEmail, "Administrador de Centro");
            await repository.assignRole(userEmail, "Administrador de Grupo");

            int userRoles = (await repository.getUserRoles(userEmail)).Count();
            userRoles.Should().Be(3);
        }

        public async Task SearchASpecificUser() {
            var repository = _factory.Server.Services.GetRequiredService<IAuthorizationServices>();
            string userEmail = "andrea.alvaradoacon@ucr.ac.cr";
            string toBeSearched = "andr";
            var matchesFound = await repository.getUsersBySearch(toBeSearched);
            matchesFound[0].Email.Should().Be(userEmail);
        }

        
    }
}