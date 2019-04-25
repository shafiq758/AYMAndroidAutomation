using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;

namespace UI_Automation_DE_UAT
{
    [TestFixture]
    public class Tests
    {
        private readonly string path = "../../../Android/bin/Debug/app-de-uat.apk";
        AndroidApp app;
        public static string EmailFinal;
        RandomGenerator generator = new RandomGenerator();

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp.Android.ApkFile(path).StartApp();
        }
        public class RandomGenerator
        {

            public string RandomString(int size, bool lowerCase)
            {
                StringBuilder builder = new StringBuilder();
                Random random = new Random();
                char ch;
                for (int i = 0; i < size; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 97)));
                    builder.Append(ch);
                }
                if (lowerCase)
                    return builder.ToString().ToLower();
                return builder.ToString();
            }

            public int RandomNumber(int min, int max)
            {
                Random random = new Random();
                return random.Next(min, max);
            }

        }
        //[Test]
        public void Repl()
        {
            app.Repl();
        }
        [Test]
        public void UI_TEST_DE_UAT()
        {
            string str = generator.RandomString(3, false);
            int rand = generator.RandomNumber(3, 100);
            string randomnumber = rand.ToString();
            string email = String.Concat(str, randomnumber);
            string remain = "@pampers.com";
            EmailFinal = String.Concat("john." ,email, remain);
            Thread.Sleep(20000);
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            Thread.Sleep(7000);
            app.Flash(x => x.Id("alreadyHaveAccountButton"));
            app.Tap(x => x.Id("joinNowButton"));
            Thread.Sleep(7000);
            app.Tap(x => x.Id("firstNameEditText"));
            app.EnterText("John");
            app.Tap(x => x.Id("textViewContent"));
            app.Tap("OK");
            app.ScrollDownTo("Email", strategy: ScrollStrategy.Gesture);
            app.Tap(x => x.Id("emailEditText"));
            app.EnterText(EmailFinal);
            app.ScrollDownTo("Password", strategy: ScrollStrategy.Gesture);
            app.Tap("Password");
            app.EnterText("magicA123");
            app.Tap(x => x.Id("joinNowButton"));
            Thread.Sleep(5000);
            app.Tap(x => x.Id("gdprContentScrollView"));
            app.ScrollDown(strategy: ScrollStrategy.Gesture);
            app.ScrollDown(strategy: ScrollStrategy.Gesture);
            app.ScrollDown(strategy: ScrollStrategy.Gesture);
            app.ScrollDown(strategy: ScrollStrategy.Gesture);
            Thread.Sleep(2000);
            app.Tap(x => x.Id("gdprCTABtn"));
            Thread.Sleep(5000);
            Logout();
            Login();
        }
        public void Login()
        {
            Thread.Sleep(10000);
            app.Tap("Email");
            app.EnterText(EmailFinal);
            app.Tap("Password");
            app.EnterText("magicA123");
            app.Tap(x => x.Id("signInButton"));
            Thread.Sleep(15000);

            var isAddCode = app.Query(x => x.Id("pop_up_title")).Any();
            if (isAddCode == true)
            {
                app.Flash(x => x.Id("dialogAddCodeBtn"));
                app.Tap(x => x.Id("notNowTextView"));
            }
            ProfileUpdate();
            BabyUpdate();
            History();         
            PamperReward();
            GetMorePoints();
            ParentsHub();
            ShowNotification();
        }
        public void ProfileUpdate()
        {
            app.Tap(x => x.Id("action_hamburger"));
            app.Tap("Pampers Account");
            app.Tap("My Details");
            app.Tap("Edit");
            app.Tap("Gender");
            app.Tap("Male");
            app.Tap("Last Name");
            app.EnterText("Smith");
            app.DismissKeyboard();
            app.Tap("Birthday");
            app.Tap(x => x.Id("date_picker_header_year"));
            app.Tap("1995");
            app.Tap("OK");
            app.Tap(x => x.Id("saveButton"));
            Thread.Sleep(10000);
            app.Flash(x => x.Id("changePasswordButton"));
            app.Tap(x => x.Id("imgBack"));
          //  app.Tap(x => x.Id("imgBack")); // uncomment when new build 
        }
        public void BabyUpdate()
        {
            Thread.Sleep(5000);
            //  app.Tap(x => x.Id("action_hamburger"));  // uncomment when new build
            //  Thread.Sleep(2000);
            app.Tap("My Baby");
            // app.Tap(x => x.Id("addChildNameButton")); // uncomment when new build
            app.Tap(x => x.Id("myBabyEditCTAButton"));  // remove when new build 

            app.Tap(x => x.Id("firstNameEditText"));
            app.EnterText("Alica");
            app.DismissKeyboard();
            Thread.Sleep(2000);
            app.Tap(x => x.Id("hintTextView"));
            app.Tap("OK");
            //  app.Tap("Baby's gender");    // uncomment when new build
            //  app.Tap("Female");           // uncomment when new build
            app.Tap(x => x.Id("saveButton"));
            Thread.Sleep(15000);
             app.Tap(x => x.Id("dueDateNextBabyTextView"));  
             app.Tap(x => x.Id("next"));                      
            app.Tap(x => x.Id("date_picker_day_picker"));   
              app.Tap("OK");                               
            Thread.Sleep(15000);
            app.Tap(x => x.Id("imgBack"));
        }
        public void History()
        {
            Thread.Sleep(5000);
            app.Tap(x => x.Id("action_hamburger"));
            Thread.Sleep(2000);
            app.Tap("History");
            Thread.Sleep(15000);
            app.Tap(x => x.Id("imgBack"));
        }
        public void PamperReward()
        {
            Thread.Sleep(5000);
            app.Tap("Featured Rewards");
            Thread.Sleep(10000);
            app.SwipeRightToLeft();
            Thread.Sleep(3000);
            app.SwipeRightToLeft();
            Thread.Sleep(3000);
            app.SwipeRightToLeft();
            Thread.Sleep(3000);
            app.SwipeRightToLeft();
            Thread.Sleep(3000);
            app.SwipeRightToLeft();
            Thread.Sleep(3000);
            app.Tap("See Rewards Here");
            Thread.Sleep(7000);
            app.Tap(x => x.Id("rewardPointField"));
            Thread.Sleep(5000);
            app.Tap(x => x.Id("rewardRedeemNowButton"));
            app.Tap(x => x.Id("placeOrderButton"));
            Thread.Sleep(30000);
            var isFailed = app.Query("Failure").Any();
            if(isFailed == true)
            {
                app.Tap("Close");
                app.Tap(x => x.Id("imgBack"));
            }
            app.Flash(x => x.Id("historyTextView"));
            app.Flash(x => x.Id("addMoreCodesTextView"));
            app.Tap(x => x.Id("backToRewardsBtn"));
            app.Tap(x => x.Id("imgBack"));
            app.Tap(x => x.Id("imgBack"));
        }
        public void GetMorePoints()
        {
            Thread.Sleep(5000);
            app.Tap("Get More Points");
            Thread.Sleep(5000);
            app.Tap(x => x.Id("homeItemActionBtn"));
            app.Tap("Cashier receipt");
            Thread.Sleep(3000);
            app.Tap(x => x.Id("receiptScanningQuickTipButton"));
            Thread.Sleep(3000);
            app.Tap(x => x.Id("closeIconImageView"));
            app.Tap(x => x.Id("backArrowImageView"));
            app.Tap("Printed/PDF invoice");
            Thread.Sleep(3000);
            app.Tap(x => x.Id("receiptScanningQuickTipButton"));
            Thread.Sleep(3000);
            app.Tap(x => x.Id("closeImageView"));
           // app.Tap(x => x.Id("backArrowImageView"));
          //  app.Tap(x => x.Id("closeImageView"));
            app.Tap(x => x.Id("imgBack"));
        }
        public void ParentsHub()
        {
            Thread.Sleep(5000);
            app.Tap("Parents' Hub");
            Thread.Sleep(5000);
            app.SwipeRightToLeft();
            Thread.Sleep(3000); 
            app.SwipeRightToLeft();
            Thread.Sleep(3000);
            app.SwipeRightToLeft();
            Thread.Sleep(3000);
            app.SwipeRightToLeft();
            Thread.Sleep(3000);
            app.SwipeRightToLeft();
            Thread.Sleep(3000);
            app.SwipeRightToLeft();
            Thread.Sleep(3000);
            app.Tap(x => x.Id("imgBack"));

        }
        public void ShowNotification()
        {
            Thread.Sleep(2000);
            app.Tap(x => x.Id("action_notification"));
            Thread.Sleep(3000);
            app.Tap(x => x.Id("showMeGoodiesBtn"));
            History();
            Logout();
        }
        public void Logout()
        {
            Thread.Sleep(15000);
            app.Tap(x => x.Id("action_hamburger"));
            app.Tap("Pampers Account");
            app.ScrollDownTo("Let's sign out", strategy: ScrollStrategy.Gesture);
            app.Tap(x => x.Id("logOutText"));
        }

    }
}
