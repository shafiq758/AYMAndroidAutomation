using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using System.Threading;
using System.Diagnostics;
using System.Text;

namespace UITest10
{
    [TestFixture]
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
               //     .ApkFile ("../../../Android/bin/Debug/app-us-uat.apk")
          //      .ApkFile("../../../Android/bin/Debug/Prod.apk")

                .StartApp();
        }
        /* [Test]
        public void repl()
        {
            app.Repl();
        }*/
          [Test]
        public void a_Register()
        {
         

            string str = generator.RandomString(10, false);
            int rand = generator.RandomNumber(5, 100);
            string randomnumber = rand.ToString();
            string email = String.Concat(str, randomnumber);
            string remain = "@pampers.com";
            final_email = String.Concat(email, remain);
            Thread.Sleep(15000);
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.Tap(x => x.Id("joinNowButton"));
            Thread.Sleep(7000);
            var isDOB = app.Query("Child's Birth / Due Date").Any();
            app.Tap(x => x.Id("firstNameEditText"));
            app.EnterText("John");
            //needs to be changed to the calender element
            app.ScrollDownTo(x => x.Id("firstNameEditText"), strategy: ScrollStrategy.Auto);
            Thread.Sleep(3000);
            if (isDOB == true)
            {
                app.Tap("Child's Birth / Due Date");
                app.Query(c => c.Class("AlertDialogLayout"));
                app.Tap("OK");
            }
            app.ScrollDownTo("Email", strategy: ScrollStrategy.Gesture, timeout: new TimeSpan(0, 1, 0));
            app.Tap("Email");
            app.Tap("Email");
            app.EnterText(final_email);
            var isZIP = app.Query("ZIP Code").Any();
            if (isZIP == true)
            {
                app.Tap("ZIP Code");
                app.EnterText("34265");
            }
            app.ScrollDown("Password");
            app.Tap("Password");
            app.EnterText("magicA123");
            app.ScrollDownTo("I'd love to join!", strategy: ScrollStrategy.Gesture, timeout: new TimeSpan(0, 1, 0));
            app.Tap("I'd love to join!");
            
            app.Query(c => c.Marked("OK").Parent().Class("AlertDialogLayout"));

            if (isDOB == false)
            {
                app.WaitForElement(x => x.Id("addDependentButton"));
                app.Tap(x => x.Id("addDependentButton"));
                app.Tap("OK");
                app.Tap(x => x.Id("onboardingDependentContinueButton"));
                app.WaitForElement("Let's lock this down");
                app.Tap("Let's lock this down");
                app.WaitForElement(x => x.Id("phoneNumberEditText"));
                app.Tap(x => x.Id("phoneNumberEditText"));
                app.EnterText("+14155552671");
                app.Tap(x => x.Id("sendSafetyCodeBtn"));
            }
            Thread.Sleep(1000);
        }


        [Test]
        public void b_reg_logout()
        {
            Thread.Sleep(10000);
           var ispresent= app.Query(x=>x.Id("dialogFragmentSecondaryButton")).Any();
            if (ispresent == true)
            {
                app.Tap(x => x.Id("dialogFragmentSecondaryButton"));
            }

            var isclosedialog = app.Query(x => x.Id("dialogFragmentPrimaryButton")).Any();
            if (isclosedialog == true)
            {
                app.Tap(x => x.Id("dialogFragmentPrimaryButton"));
            }
            d_reg_logout();


        }
        [Test]
        public void login() {

            Thread.Sleep(20000);
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.Tap(x => x.Id("alreadyHaveAccountButton"));
            app.Tap("Email");
            app.EnterText(final_email);
            app.Tap("Password");
            app.EnterText("magicA123");
            app.ScrollDownTo("Sign me in!", strategy: ScrollStrategy.Gesture, timeout: new TimeSpan(0, 1, 0));
            app.Tap(x => x.Id("signInButton"));
            Thread.Sleep(10000);

            var isAccountSafeBox = app.Query("Keep your account (and your points!) extra safe").Any();
            if(isAccountSafeBox == true)
            {
                app.Tap("I'll skip this");
            }
            var isAddCode = app.Query(x => x.Id("pop_up_title")).Any();
            if (isAddCode == true)
            {
                app.Flash("Add code now");
                app.Tap("Not now");
            }
            b_ProfileUpdate();
            i_anotherchild();
            c_pamper_reward();
            e_menubar();
            f_MyDetails();
            g_changepassword();
            home_ParentsHub();
           // home_GetMorePoints();
            d_logout();
        }


        public void i_anotherchild() {

            Thread.Sleep(25000);
            app.Tap("I've had another baby");
            app.Tap("Child's First Name");
            app.EnterText("Alica ");
            app.Tap("Child's Birth / Due Date");
            app.Tap("Next month");
            app.Tap(x => x.Id("date_picker_day_picker"));
            app.Tap("OK");
            app.Tap("Save");
            Thread.Sleep(10000);
            // app.Visibility("Edit");
            app.WaitForElement("Edit");


        }
        public void e_menubar()
        {
            bool result;
            Thread.Sleep(15000);
            app.Tap(x => x.Id("imgBack"));
            app.Tap(x => x.Id("imgBack"));
            app.Tap(x => x.Id("action_hamburger"));
            Thread.Sleep(10000);
            string data = app.Query(c => c.Id("navItemText"))[0].Text;
            string data1 = app.Query(c => c.Id("navItemText"))[1].Text;
            string data2 = app.Query(c => c.Id("navItemText"))[2].Text;
            var ispamper = app.Query("Pampers Account").Any();
            if (ispamper == true)
                result = data.Equals("Pampers Account");
            else
                result = data.Equals("Pampers account");
            
            bool result1 = data1.Equals("History");
            bool result2 = data2.Equals("Help");
            result.Equals(result1.Equals(result2));
            Console.WriteLine(data);
            Console.WriteLine(data1);
            Console.WriteLine(data2);
            //app.Equals(data);


        }

        public void b_ProfileUpdate()
        {

            Thread.Sleep(15000);
            app.Tap(x => x.Id("action_hamburger"));
            Thread.Sleep(10000);
            //app.Tap("Pampers Account");
            var ispamper = app.Query("Pampers Account").Any();
            if (ispamper == true)
                app.Tap("Pampers Account");
            else
                app.Tap("Pampers account");

            //app.Qnuery(c => c.Marked("Pampers Account").Parent().Class("AlertDialogLayout"));
            app.Tap("My Baby");
            app.Tap("Edit");
            app.Tap("Child's First Name");
            //app.Query(c => c.Marked("Male").Parent().Class("AlertDialogLayout"));
            //app.Tap("First Name");
            app.EnterText("Baby Name");
            app.Tap("My baby's birthday");
            //app.Tap("Khan");
            //app.Tap("Birthday");
            app.Query(c => c.Marked("OK").Parent().Class("AlertDialogLayout"));
            app.Tap("OK");
            app.DismissKeyboard();
            app.Tap("Save");



        }
        public void f_MyDetails()
        {

            //Thread.Sleep(15000);
            //app.Tap(x => x.Id("action_hamburger"));
            //Thread.Sleep(10000);
            var ispamper = app.Query("Pampers Account").Any();
            if (ispamper == true)
                app.Tap("Pampers Account");
            else
                app.Tap("Pampers account");

            app.Tap(x => x.Id("imgBack"));
            app.Tap("My Details");
            app.Tap("Edit");
            app.Tap("Gender");
            app.Tap("Male");
            app.Tap("Last Name");
            app.EnterText("Smith");
            app.Tap("Birthday");
            app.Tap(x => x.Id("date_picker_header_year"));
            app.Tap("1997");
            app.Tap("OK");
            app.ScrollDownTo(x => x.Id("saveButton"), strategy: ScrollStrategy.Auto);
            app.Tap(x => x.Id("saveButton"));
            Thread.Sleep(10000);
            // app.Visibility("Edit");
            app.WaitForElement("Edit");




        }



        public void c_pamper_reward()
        {

            Thread.Sleep(15000);
            app.Tap(x => x.Id("action_home"));


            app.Tap("Featured Rewards");
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.SwipeRightToLeft();
            app.Tap("See Rewards Here");
            app.Tap(x => x.Id("action_reward"));
            Thread.Sleep(10000);
            app.Tap("Shutterfly Prints Package");
            Thread.Sleep(10000);
            app.Tap(x => x.Id("rewardRedeemNowButton"));
            app.ScrollDownTo("Great! Place my order", strategy: ScrollStrategy.Gesture, timeout: new TimeSpan(0, 1, 0));
            //app.Tap(x => x.Id("placeOrderButton"));



        }

        public void d_logout()
        {

            Thread.Sleep(15000);
          //  app.Tap(x => x.Id("imgBack"));
           // app.Tap(x => x.Id("imgBack"));
            ////id for below
            app.Tap(x => x.Id("action_hamburger"));
            //app.Tap(x => x.Id("navAccountItem"));
            var ispamper = app.Query("Pampers Account").Any();
            if (ispamper == true)
                app.Tap("Pampers Account");
            else
                app.Tap("Pampers account");
            // app.Tap(x => x.Id("imgBack"));
            app.ScrollDownTo("Let's sign out", strategy: ScrollStrategy.Gesture, timeout: new TimeSpan(0, 1, 0));

            app.Tap(x => x.Id("logOutText"));

        }

        public void d_reg_logout()
        {

            Thread.Sleep(15000);
            
            app.Tap(x => x.Id("action_hamburger"));
            //app.Tap(x => x.Id("navAccountItem"));
            var ispamper = app.Query("Pampers Account").Any();
            if (ispamper == true)
                app.Tap("Pampers Account");
            else
                app.Tap("Pampers account");
            // app.Tap(x => x.Id("imgBack"));
            app.ScrollDownTo("Let's sign out", strategy: ScrollStrategy.Gesture, timeout: new TimeSpan(0, 1, 0));

            app.Tap(x => x.Id("logOutText"));

        }
        public void g_changepassword()
        {

            //Thread.Sleep(15000);
           // app.Tap(x => x.Id("action_hamburger"));
            //Thread.Sleep(10000);
           // app.Tap("Pampers Account");
            app.Tap("Change my password");
            app.Tap("Close");


        }
        public void home_ParentsHub()
        {
            Thread.Sleep(15000);
            app.Tap(x => x.Id("action_home"));
            app.Tap("Parents' Hub");
            Thread.Sleep(10000);

            app.SwipeRightToLeft();
            Thread.Sleep(5000);

            app.SwipeRightToLeft();
            Thread.Sleep(5000);

            app.SwipeRightToLeft();
            Thread.Sleep(5000);

            app.SwipeRightToLeft();
            Thread.Sleep(5000);

            app.SwipeRightToLeft();
            app.Tap(x => x.Id("imgBack"));
        }
        
        public void home_GetMorePoints()
        {
            app.Tap("Get More Points");
            app.Tap("homeItemActionBtn");
            app.Tap(x => x.Id("diaperImageView"));
            app.Tap(x=>x.Id("scanningGuideCardView1"));
            app.Tap(x => x.Id("scanningGuideCardView1"));
            app.Tap(x => x.Id("scanningGuideCardView2"));
            app.Tap(x => x.Id("scanningGuideCardView2"));

            app.Tap(x => x.Id("gotItButton"));
            app.Tap(x => x.Id("manualEntryImageView"));
            app.Tap(x => x.Id("textInputEditText"));
            app.EnterText("TESTKEYCODE5");
            app.Tap(x => x.Id("submitCodeButton"));
            var isSuccess = app.Query("High-five!You just got").Any();
            if(isSuccess== true)
            {

                app.Flash(x => x.Id("scanAnotherCodeButton"));
                app.Flash(x => x.Id("wantAnotherButton"));
                app.Tap(x => x.Id("closeImageView"));
            }
            //var isCodeUsed = app.Query(x => x.Id("errorTypeTextView")).Any();
            //if( isCodeUsed == true)
            //{
            //   // app.Flash(x => x.Id("actionButton"));
                

            //}
            //var isFailed = app.Query(x => x.Id("errorTypeTextView")).Any();  
            //if (isFailed == true)
            //{
            //    app.Flash("Try Again");
            //    app.Flash("Help me find codes");
            //    app.Tap(x => x.Id("closeImageView"));
            //}
            app.Tap(x => x.Id("closeImageView"));
            app.Tap("homeItemActionBtn");
            app.Tap(x => x.Id("wipeImageView"));
            var isWipesBox = app.Query(x=>x.Id("scan_wipes_dialog_manual_btn")).Any();
            if(isWipesBox == true)
            {
                app.Tap(x => x.Id("scan_wipes_dialog_manual_btn"));
            }
            app.Tap(x => x.Id("scanningGuideCardView1"));
            app.Tap(x => x.Id("scanningGuideCardView1"));
            app.Tap(x => x.Id("scanningGuideCardView2"));
            app.Tap(x => x.Id("scanningGuideCardView2"));
            app.Tap(x => x.Id("gotItButton"));
           // app.Flash("submitCodeButton");
            app.Tap(x => x.Id("closeImageView"));
        }
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }
    }
}

