using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deque.AxeCore.Commons;
using Deque.AxeCore.Playwright;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace BlazorDemoApp.Tests.CardTests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class CardExampleTest : PageTest
    {
        [SetUp]
        public async Task Setup()
        {
            await Context.Tracing.StartAsync(new()
                                             {
                                                 Title = TestContext.CurrentContext.Test.Name,
                                                 Screenshots = true,
                                                 Snapshots = true,
                                                 Sources = true
                                             });
        }

        [TearDown]
        public async Task TearDown()
        {
            await Context.Tracing.StopAsync(new()
            {
                Path = Path.Combine("C:","traces", "card-trace.zip")
            });
        }

        [Test]
        public async Task VerifyCorrectRendering()
        {
            await Page.GotoAsync("https://localhost:7284/");
            await Page.GotoAsync("https://localhost:7284/?path=/docs/button-button--docs");
            await Page.GetByRole(AriaRole.Button, new() { Name = "CardExample" }).ClickAsync();
            var iframe = Page.FrameLocator("iframe").First;

            await Expect(iframe.GetByAltText("card-image")).ToBeVisibleAsync();
            await Expect(iframe.GetByAltText("card-title")).ToBeVisibleAsync();
            await Expect(iframe.GetByAltText("card-text")).ToBeVisibleAsync();
            await Expect(iframe.GetByAltText("card-button")).ToBeVisibleAsync();
        }

        [Test]
        public async Task CheckAccesibilityOfPage()
        {
            AxeRunOptions options = new()
                                    {
                                        RunOnly = RunOnlyOptions.Tags("wcag1aa")
                                    };

            await CheckAssesibility(options);
        }

        private async Task CheckAssesibility(AxeRunOptions options)
        {
            await Page.GotoAsync("https://localhost:7284/");
            await Page.GotoAsync("https://localhost:7284/?path=/docs/button-button--docs");
            await Page.GetByRole(AriaRole.Button, new() { Name = "CardExample" }).ClickAsync();
            var iframe = Page.FrameLocator("iframe").First;

            var accesibilityObject = await Page.RunAxe(options);
            Assert.That(accesibilityObject.Violations, Is.Null.Or.Empty);
        }
    }
}
