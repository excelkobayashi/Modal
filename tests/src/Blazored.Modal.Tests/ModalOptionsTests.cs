﻿using Blazored.Modal.Services;
using Blazored.Modal.Tests.Assets;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Blazored.Modal.Tests
{
	public class ModalOptionsTests : ComponentTestFixture
	{
		public ModalOptionsTests()
		{
			Services.AddScoped<NavigationManager, MockNavigationManager>();
			Services.AddBlazoredModal();
		}

		[Fact]
		public void ModalDisplaysSpecifiedTitle()
		{
			// Arrange
			var testTitle = "Title";
			var modalService = Services.GetService<IModalService>();
			var cut = RenderComponent<BlazoredModal>();

			// Act
			modalService.Show<TestComponent>(testTitle);

			// Assert
			Assert.Equal(testTitle, cut.Find(".blazored-modal-title").InnerHtml);
		}

		[Fact]
		public void ModalDisplaysCustomStyles()
		{
			// Arrange
			var modalService = Services.GetService<IModalService>();
			var customStyle = "my-custom-style";
			var options = new ModalOptions { Class = customStyle };
			var cut = RenderComponent<BlazoredModal>();

			// Act
			modalService.Show<TestComponent>("", options);

			// Assert
			Assert.NotNull(cut.Find($"div.{customStyle}"));
		}

		[Fact]
		public void ModalDisplaysCloseButtonByDefault()
		{
			// Arrange
			var modalService = Services.GetService<IModalService>();
			var cut = RenderComponent<BlazoredModal>();

			// Act
			modalService.Show<TestComponent>();

			// Assert
			Assert.NotNull(cut.Find(".blazored-modal-close"));
		}

		[Fact]
		public void ModalDoesNotDisplayCloseButtonWhenSetToFalseInOptions()
		{
			// Arrange
			var modalService = Services.GetService<IModalService>();
			var options = new ModalOptions { HideCloseButton = true };
			var cut = RenderComponent<BlazoredModal>();

			// Act
			modalService.Show<TestComponent>("", options);

			// Assert
			Assert.Empty(cut.FindAll(".blazored-modal-close"));
		}

		[Fact]
		public void ModalDisplaysHeaderByDefault()
		{
			// Arrange
			var modalService = Services.GetService<IModalService>();
			var cut = RenderComponent<BlazoredModal>();

			// Act
			modalService.Show<TestComponent>();

			// Assert
			Assert.NotNull(cut.Find(".blazored-modal-header"));
		}

		[Fact]
		public void ModalDoesNotDisplayHeaderWhenSetToFalseInOptions()
		{
			// Arrange
			var modalService = Services.GetService<IModalService>();
			var options = new ModalOptions { HideHeader = true };
			var cut = RenderComponent<BlazoredModal>();

			// Act
			modalService.Show<TestComponent>("", options);

			// Assert
			Assert.Empty(cut.FindAll(".blazored-modal-header"));
		}

		[Fact]
		public void ModalDisplaysCorrectContent()
		{
			// Arrange
			var modalService = Services.GetService<IModalService>();
			var cut = RenderComponent<BlazoredModal>();

			// Act
			modalService.Show<TestComponent>("");

			// Assert
			Assert.Equal(TestComponent.DefaultTitle, cut.Find(".test-component h1").InnerHtml);
		}

		[Fact]
		public void ModalDisplaysCorrectContentWhenUsingModalParameters()
		{
			var testTitle = "Testing Components";

			// Arrange
			var modalService = Services.GetService<IModalService>();

			var parameters = new ModalParameters();
			parameters.Add("Title", testTitle);
			var cut = RenderComponent<BlazoredModal>();

			// Act
			modalService.Show<TestComponent>("", parameters);

			// Assert
			Assert.Equal(testTitle, cut.Find(".test-component h1").InnerHtml);
		}
	}
}
