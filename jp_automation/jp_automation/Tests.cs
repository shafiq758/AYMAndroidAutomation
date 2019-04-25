using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using System.Threading;
using System.Text;
using System.Collections.Generic;

namespace jp_automation
{
    
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

    [TestFixture]
    public class Tests
    {
        public static string final_email;
        RandomGenerator generator = new RandomGenerator();
       
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            // TODO: If the Android app being tested is included in the solution then open
            // the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            app = ConfigureApp
                .Android
                // TODO: Update this path to point to your Android app and uncomment the
                // code if the app is not included in the solution.
                //.ApkFile ("../../../Android/bin/Debug/app-jp-release.apk") // Prod
                  .ApkFile ("../../../Android/bin/Debug/app-jp-uat.apk") // UAT
                .StartApp();

         // app.Repl(); // Using for tree
        }

        public void Wait()
        {
            Thread.Sleep(20000); // 20 sec
        }


        [Test]
        public void a_Registration()
        {
            // Random User from List
            string[] User_Family_Name = { "John", "Jack", "Harry", "Charlie" };
            Random rand_user = new Random();
            int Index_Family = rand_user.Next(User_Family_Name.Length);

            // Random email generate and con 
           //  string str = generator.RandomString(10, false);
            int rand = generator.RandomNumber(5, 100);
            string randomnumber = rand.ToString();
            string email = String.Concat("Taha+1", randomnumber);
            string remain = "@aymcommerce.com";
            final_email = String.Concat(email, remain);

            Console.WriteLine("===============Registration==============");
            Wait(); 
            // Wait for splash loading
                    //app.SwipeRightToLeft();
                    //  app.SwipeRightToLeft();
                    // app.SwipeRightToLeft();
            app.Flash(x => x.Id("headerTextView"));
            app.Flash(x => x.Id("animationView"));
            Thread.Sleep(5000);
            app.SwipeRightToLeft();
            app.Flash(x => x.Id("headerTextView"));
            app.Flash(x => x.Id("animationView"));
            Thread.Sleep(5000);
            app.SwipeRightToLeft();
            app.Flash(x => x.Id("headerTextView"));
            app.Flash(x => x.Id("animationView"));
            Thread.Sleep(5000);
            app.SwipeRightToLeft();
            app.Flash(x => x.Id("headerTextView"));
            app.Flash(x => x.Id("animationView"));
            Wait();
            Console.WriteLine("===============Swiping Completed==============");
            app.Flash("joinNowButton"); // Checking Join now button
            app.Flash("alreadyHaveAccountButton"); // Checking I already have an account 

            app.Tap(x => x.Id("joinNowButton"));
            app.Tap(x => x.Id("familyNameEditText"));
            app.EnterText(User_Family_Name[Index_Family]);
            app.Tap(x => x.Id("firstNameEditText"));
            app.EnterText(User_Family_Name[Index_Family]);
            // app.Tap("Child's Birthday / Due Date");
            app.DismissKeyboard();
            Thread.Sleep(5000);
            app.Tap(x => x.Id("textViewContent"));
            app.Query(c => c.Class("AlertDialogLayout"));
            app.Tap("OK");
            app.ScrollDown();
            // app.Tap("Email");
            app.Tap(x => x.Id("emailEditText"));
          //  app.EnterText("taha+92@aymcommerce.com");
            app.EnterText(final_email);
            //   app.Tap("Password");
            app.ScrollDown();
            app.Tap(x => x.Id("passwordEditText"));
            app.EnterText("password@123");
            Thread.Sleep(5000);
            app.DismissKeyboard();
            app.Tap(x => x.Id("joinNowButton"));
            Thread.Sleep(10000);
            
            // Already have an acount
            var alreadyhaveaccount = app.Query(x => x.Id("genericDialogContainer")).Any();
            Thread.Sleep(10000);
            if (alreadyhaveaccount == true)
            {
                app.Tap(x => x.Id("dialogFragmentPrimaryButton"));
                Thread.Sleep(10000);
                BeforeEachTest();
                a_Registration();
            }

            Console.WriteLine("===============User registration Completed==============");
            Thread.Sleep(80000);
            var dontknow = app.Query(x => x.Id("pop_up_title")).Any();
            if (dontknow == true)
            {
                app.Tap(x => x.Id("notNowTextView"));
            }

            missingtoken();
        }


        [Test]
        public void b_Login()
        {
            Console.WriteLine("===============App Start==============");
            Thread.Sleep(20000); // splash screen wait
            Console.WriteLine("===============Swiping Start==============");
            app.SwipeRightToLeft();
            Thread.Sleep(5000);
            app.SwipeRightToLeft();
            Thread.Sleep(5000);
            app.SwipeRightToLeft();
            Thread.Sleep(5000);
            Console.WriteLine("===============Swiping Completed==============");
            Console.WriteLine("======Login=======");
            app.Tap(x => x.Id("alreadyHaveAccountButton"));
            // app.Tap("Email");
            app.Tap(x => x.Id("textInputEditText"));
            // app.Tap(x=> x.Id("Email"));
           //  app.EnterText("taha+92@aymcommerce.com"); // for testing i used static email
           app.EnterText(final_email); // dynamic email address
            app.Tap(x => x.Id("passwordEditText"));
            //  app.Tap("Password");
          //  app.Tap(x => x.Id("Email"));
            app.EnterText("password@123");
            // app.ScrollDownTo("Sign me in!", strategy: ScrollStrategy.Gesture, timeout: new TimeSpan(0, 1, 0));
            app.DismissKeyboard();
            app.Tap(x => x.Id("signInButton"));
            Thread.Sleep(15000);
            var dontknow = app.Query(x => x.Id("pop_up_title")).Any();
            if (dontknow == true)
            {
                app.Tap(x => x.Id("notNowTextView"));
            }

            // missingtoken();
            c_HomeTab();
            d_Userinfomation();
            e_Mybaby();
            f_redeemptionHistory();
            h_Getmorerewards();
            i_Rewards();
            j_resetpassowrd();

        }

       
        public void c_HomeTab()
        {
            app.Tap(x => x.Id("homeItemTitle").Index(0));
            app.Tap(x => x.Id("imgBack"));

            app.Tap(x => x.Id("homeItemTitle").Index(1));
            app.Tap(x => x.Id("imgBack"));

            app.Tap(x => x.Id("homeItemTitle").Index(2));
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.Tap(x => x.Id("imgBack"));

            app.Tap(x => x.Id("userNameTextView"));
            app.Tap(x => x.Id("imgBack"));

            app.Tap(x => x.Id("memberPointsToLevel"));
            Thread.Sleep(15000);
            app.Tap(x => x.Id("imgBack"));

            app.Tap(x => x.Id("action_notification"));
            app.Tap(x => x.Id("action_add_code"));
            Thread.Sleep(15000);
            app.Tap(x => x.Id("closeImageView"));
            app.Tap(x => x.Id("action_reward"));

            app.SwipeLeftToRight();
        }

        public void d_Userinfomation()
        {
            int year;
            Thread.Sleep(15000);
            app.Tap(x => x.Id("action_hamburger"));
            Thread.Sleep(10000);
            app.Tap(x => x.Id("navItemText").Index(0)); // Tap on pamper account
            app.Tap(x => x.Id("userInfoOption")); // User information tab
            app.Tap(x => x.Id("label")); // edit button
            app.Tap(x => x.Id("textViewContent").Index(0)); // gender
            app.Tap(x => x.Id("text1").Index(1)); //  0 for male and 1 for female
            app.Tap(x => x.Id("textViewContent").Index(1)); // Birthday
            for (year = 0; year <= 2; year++)
            {
                app.Tap(x => x.Id("prev"));

            }
            Thread.Sleep(5000);
            app.Tap(x => x.Id("button1")); // calender ok button

            app.Tap(x => x.Id("label")); // user info save button
            Thread.Sleep(30000);
            app.Tap(x => x.Id("label").Index(1)); // Change passoword
            Thread.Sleep(30000);
            app.Tap(x => x.Id("closeTextView"));
        }

        public void e_Mybaby()
        {
            Thread.Sleep(10000); // for loading
            // Add first baby
            //app.Tap(x => x.Id("addEmptyBabyBtn")); // Add a baby
           // app.Tap(x => x.Id("firstNameEditText")); // add first baby
           // app.EnterText("Aliza");
           // app.Tap(x => x.Id("childBirthdayPicker"));
          //  app.Tap(x => x.Id("prev")); // 3 year previous
           // app.Tap(x => x.Id("prev"));
           // app.Tap(x => x.Id("prev"));
         //   Thread.Sleep(5000);
           // app.Tap(x => x.Id("button1"));
          //  app.Tap(x => x.Id("label")); // save button
          //  Thread.Sleep(10000);
             app.Tap(x=>x.Id("myBabyOption"));
            // Add another child
            app.Tap(x => x.Id("addAnotherChildButton"));
            app.Tap(x => x.Id("firstNameEditText")); // add first baby
            app.EnterText("Xoxo");
            app.Tap(x => x.Id("childBirthdayPicker"));
            app.Tap(x => x.Id("prev")); // 3  previous
            Thread.Sleep(15000);
            app.Tap(x => x.Id("button1"));
            app.Tap(x => x.Id("label")); // save button
            Thread.Sleep(10000);

            //Add Due date for next babay
            app.Tap(x => x.Id("dueDateNextBabyTextView"));
            for (int i=0; i<=6; i++)
            {
                app.Tap(x => x.Id("next"));
            }
            app.Tap(x => x.Id("button1"));
            Thread.Sleep(20000);
            app.Tap(x => x.Id("imgBack"));
            app.ScrollDown();
        }

        public void f_redeemptionHistory()
        {
            Thread.Sleep(10000);
            app.Tap(x => x.Id("redemptionHistoryOption"));
            Thread.Sleep(5000);
            app.Tap(x => x.Id("imgBack"));

        }

        public void g_History()
        {
            app.Tap(x => x.Id("navItemText").Index(1)); // History tab
            var data =  app.Flash("pointEarnedTextView");
            Console.WriteLine(data);
            app.Tap(x => x.Id("imgBack"));
        }

        public void h_Getmorerewards()
        {
            //Random Code Generation 
            Random random = new Random();
            var Codes = new List<string>
            {
                "NCYY6XAA11M8Y20K",
                "ARE5E93N6YT13YKC",
                "RG9MYN95C0XEHNRH",
                "634XMR31X7G6G6GY",
                "LC44KE74TA3AT39X",
                "T8LFTHK28EEGRTK2",
                "T6FKE3F3HH3AXCHK",
                "CF9XYFX85R42G4MR",
                "N0G4RXHG82FLTTNF"
            };
            int index = random.Next(Codes.Count);
            Thread.Sleep(5000);
            app.Tap(x => x.Id("action_add_code"));
            app.Tap(x => x.Id("letsScanBtn"));
            app.Tap(x => x.Id("manualEntryContainer"));
            app.Tap(x => x.Id("titleTextView"));
            app.EnterText(Codes[index]);
            //app.EnterText("53Mjksuswqwaq");
            app.Tap(x => x.Id("manualEntryOKBtn"));
            Thread.Sleep(30000);
            app.WaitForElement(e => e.Id("closeImageView"));
            var isSuccess = app.Query(x => x.Id("pointEarnedTextView")).Any();
            if (isSuccess == true)
            {
                app.Tap(x => x.Id("closeImageView"));
            }
            var isCodeUsed = app.Query(x => x.Id("errorTypeTextView")).Any();
            if (isCodeUsed == true)
            {
                app.Tap(x => x.Id("closeImageView"));

            }
            var isFailed = app.Query(x => x.Id("errorTypeTextView")).Any();
            if (isFailed == true)
            {
                app.Tap(x => x.Id("closeImageView"));
            }

                app.Tap(x => x.Id("closeImageView"));

        }

        public void i_Rewards()
        {
            Thread.Sleep(10000);
            app.Tap(x => x.Id("action_reward"));
            app.Tap(x => x.Id("rewardPointField"));
            app.Tap(x => x.Id("rewardRedeemNowButton"));
            app.Tap(x => x.Id("zipCodeEditText"));
            app.EnterText("182-0000");
            app.Tap(x => x.Id("textViewContent"));
            app.Tap(x => x.Id("text1"));
            app.DismissKeyboard();
            app.Tap(x => x.Id("cityEditText"));
            app.EnterText("Tokyo");
            app.ScrollDown();
            app.Tap(x => x.Id("addressLine1EditText"));
            app.EnterText("abcd");
            app.Tap(x => x.Id("phoneNumberEditText"));
            app.EnterText("1234567890");

            app.Tap(x => x.Id("saveAndContinueBtn"));
            Thread.Sleep(10000);
            app.ScrollDown();
            app.Tap(x => x.Id("placeOrderButton"));
            Thread.Sleep(20000);

            app.Tap(x => x.Id("backToRewardsBtn"));
            app.Tap(x => x.Id("imgBack"));

            app.Tap(x => x.Id("action_home"));

            app.Tap(x => x.Id("action_hamburger"));
            app.Tap(x => x.Id("navItemText").Index(1));
            Thread.Sleep(10000);
            app.Tap(x => x.Id("action_home"));

            logout();

        }

        public void j_resetpassowrd()
        {
            Thread.Sleep(10000);
            app.ScrollDown();
            app.DismissKeyboard();
            app.Tap(x => x.Id("forgotPasswordTextView"));
            app.Tap(x => x.Id("emailEditText"));

          //  app.EnterText("taha+92@aymcommerce.com");

            app.EnterText(final_email);
            app.DismissKeyboard();
            app.Tap(x => x.Id("resetPassword"));
            Thread.Sleep(10000);
            app.Tap(x => x.Id("closeTextView"));
        }

        public void missingtoken()
        {
            Thread.Sleep(15000);
            var isMissingToken = app.Query(x => x.Id("missing_token")).Any();
            if (isMissingToken == true)
            {
                app.Tap("Close");
            }
            var ispresent = app.Query(x => x.Id("dialogFragmentSecondaryButton")).Any();
            if (ispresent == true)
            {
                app.Tap(x => x.Id("dialogFragmentSecondaryButton"));
            }
            var isclosedialog = app.Query(x => x.Id("dialogFragmentPrimaryButton")).Any();
            if (isclosedialog == true)
            {
                app.Tap(x => x.Id("dialogFragmentPrimaryButton"));
            }
            logout();
        }

        public void logout()
        {
            Thread.Sleep(15000);
            app.Tap(x => x.Id("action_hamburger"));
            //app.Tap("Pampers Account");
            app.Tap(x => x.Id("navItemText"));
            app.ScrollDown();
            app.Tap(x => x.Id("logOutText"));
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }
    }
}

