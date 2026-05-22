using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SponsorshipWorkflow.Data;

public class SponsorshipWorkflowDataSeedContributor : IDataSeedContributor, ITransientDependency
{
	private readonly IGuidGenerator _guidGenerator;
	private readonly IIdentityUserRepository _userRepository;
	private readonly IIdentityRoleRepository _roleRepository;
	private readonly IdentityUserManager _userManager;

	public SponsorshipWorkflowDataSeedContributor(
		IGuidGenerator guidGenerator,
		IIdentityUserRepository userRepository,
		IIdentityRoleRepository roleRepository,
		IdentityUserManager userManager)
	{
		_guidGenerator = guidGenerator;
		_userRepository = userRepository;
		_roleRepository = roleRepository;
		_userManager = userManager;
	}

	[UnitOfWork]
	public virtual async Task SeedAsync(DataSeedContext context)
	{
		var roles = new[] { "Requestor", "Manager", "FinanceAdmin", "SystemAdmin" };
		foreach (var roleName in roles)
		{
			var existing = await _roleRepository.FindByNormalizedNameAsync(roleName.ToUpperInvariant());
			if (existing == null)
			{
				var role = new IdentityRole(_guidGenerator.Create(), roleName);
				await _roleRepository.InsertAsync(role);
			}
		}

		await CreateUserIfNotExistsAsync("requestor@test.com", "Requestor");
		await CreateUserIfNotExistsAsync("manager@test.com", "Manager");
		await CreateUserIfNotExistsAsync("finance@test.com", "FinanceAdmin");
		await CreateUserIfNotExistsAsync("admin@test.com", "SystemAdmin");
	}

	protected virtual async Task CreateUserIfNotExistsAsync(string email, string role)
	{
		var normalizedEmail = email.ToUpperInvariant();
		var existing = await _userRepository.FindByNormalizedEmailAsync(normalizedEmail);
		if (existing != null) return;

		var user = new IdentityUser(_guidGenerator.Create(), email, normalizedEmail)
		{
			// Note: IdentityUser properties such as Email set via constructor in this ABP version
		};

		var result = await _userManager.CreateAsync(user, SponsorshipWorkflowConsts.AdminPasswordDefaultValue);
		if (!result.Succeeded) return;

		await _userManager.AddToRoleAsync(user, role);
	}
}
