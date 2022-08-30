using _archive.Models;

namespace _archive.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                //Creating Database if not exists
                context.Database.EnsureCreated();


                /*
                 NOTE: Creating UsersModel first is important. It throws exception error if we did not create it first
                 because of FOREIGN KEY Issiues with Records.
                 */
                if (!context.UsersModel.Any())
                {
                    context.UsersModel.AddRange(new List<UsersModel>
                    {
                        new UsersModel()
                        {

                            UserName = "Test User 1",
                            Email = "test1@outlook.com",
                            Password = "test1",
                            RegisteredAt = DateTime.Now,
                            LastLogin = DateTime.Now,
                            Role = Enums.UsersRoles.Admin
                        },
                        new UsersModel()
                        {

                            UserName = "Test User 2",
                            Email = "test2@outlook.com",
                            Password = "test2",
                            RegisteredAt = DateTime.Now,
                            LastLogin = DateTime.Now,
                            Role = Enums.UsersRoles.User
                        },
                        new UsersModel()
                        {

                            UserName = "Test User 3",
                            Email = "test3@outlook.com",
                            Password = "test3",
                            RegisteredAt = DateTime.Now,
                            LastLogin = DateTime.Now,
                            Role = Enums.UsersRoles.User
                        }
                    });
                    context.SaveChanges();
                }


                //Creating Arcive Data
                if (!context.RecordsModel.Any())
                {
                    context.RecordsModel.AddRange(new List<RecordsModel>
                    {
                        new RecordsModel()
                        {
                           
                            ChangesetID = 121,
                            Title="Test 1 Title",
                            UpdateDetails = "Test 1 Update Details Review",
                            Analysis =" Test 1 Analysis DetailsReview",
                            BPMNo = 1231,
                            TestResults = "Test 1 Results are shown as failure",
                            StartTime = DateTime.Now,   
                            EndTime = new DateTime(2022,9,1),
                            ReleaseTime = new DateTime(2022,10,1),
                            Status = "Failed",
                            RecordsCategory = Enums.RecordsCategory.KOTLIN,
                            UserID = 1

                        },
                         new RecordsModel()
                        {
                           
                            ChangesetID = 122,
                            Title="Test 2 Title",
                            UpdateDetails = "Test 2 Update Details Review",
                            Analysis =" Test 2 Analysis DetailsReview",
                            BPMNo = 1232,
                            TestResults = "Test 2 Results are shown as failure",
                            StartTime = DateTime.Now,
                            EndTime = new DateTime(2022,9,2),
                            ReleaseTime = new DateTime(2022,10,2),
                            Status = "Failed",
                            RecordsCategory = Enums.RecordsCategory.FLUTTER,
                            UserID = 2

                        },
                          new RecordsModel()
                        {
                           
                            ChangesetID = 123,
                            Title="Test 3 Title",
                            UpdateDetails = "Test 3 Update Details Review",
                            Analysis =" Test 3 Analysis DetailsReview",
                            BPMNo = 1233,
                            TestResults = "Test 3 Results are shown as failure",
                            StartTime = DateTime.Now,
                            EndTime = new DateTime(2022,9,3),
                            ReleaseTime = new DateTime(2022,10,3),
                            Status = "Failed",
                            RecordsCategory = Enums.RecordsCategory.KOTLIN,
                            UserID = 3

                        },
                          new RecordsModel()
                        {

                            ChangesetID = 124,
                            Title="Test 4 Title",
                            UpdateDetails = "Test 4 Update Details Review",
                            Analysis =" Test 4 Analysis DetailsReview",
                            BPMNo = 1234,
                            TestResults = "Test 4 Results are shown as failure",
                            StartTime = DateTime.Now,
                            EndTime = new DateTime(2022,9,4),
                            ReleaseTime = new DateTime(2022,10,4),
                            Status = "Failed",
                            RecordsCategory = Enums.RecordsCategory.FLUTTER,
                            UserID = 2

                        },
                          new RecordsModel()
                        {

                            ChangesetID = 125,
                            Title="Test 5 Title",
                            UpdateDetails = "Test 5 Update Details Review",
                            Analysis =" Test 5 Analysis DetailsReview",
                            BPMNo = 1235,
                            TestResults = "Test 5 Results are shown as failure",
                            StartTime = DateTime.Now,
                            EndTime = new DateTime(2022,9,5),
                            ReleaseTime = new DateTime(2022,10,5),
                            Status = "Failed",
                            RecordsCategory = Enums.RecordsCategory.FLUTTER,
                            UserID = 3

                        },
                          new RecordsModel()
                        {

                            ChangesetID = 126,
                            Title="Test 6 Title",
                            UpdateDetails = "Test 6 Update Details Review",
                            Analysis =" Test 6 Analysis DetailsReview",
                            BPMNo = 1236,
                            TestResults = "Test 6 Results are shown as failure",
                            StartTime = DateTime.Now,
                            EndTime = new DateTime(2022,9,6),
                            ReleaseTime = new DateTime(2022,10,6),
                            Status = "Failed",
                            RecordsCategory = Enums.RecordsCategory.KOTLIN,
                            UserID = 1

                        },
                          new RecordsModel()
                        {

                            ChangesetID = 127,
                            Title="Test 7 Title",
                            UpdateDetails = "Test 7 Update Details Review",
                            Analysis =" Test 7 Analysis DetailsReview",
                            BPMNo = 1237,
                            TestResults = "Test 7 Results are shown as failure",
                            StartTime = DateTime.Now,
                            EndTime = new DateTime(2022,9,7),
                            ReleaseTime = new DateTime(2022,10,7),
                            Status = "Failed",
                            RecordsCategory = Enums.RecordsCategory.KOTLIN,
                            UserID = 3

                        }
                    });
                    context.SaveChanges();
                }

                
            }
        }
    }
}
