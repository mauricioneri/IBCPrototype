namespace IBC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VersaoInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DogCategory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .Index(t => t.UserProcessingId);
            
            CreateTable(
                "dbo.DogBreed",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .Index(t => t.UserProcessingId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Contest_Id = c.Guid(),
                        Contest_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contest", t => t.Contest_Id)
                .ForeignKey("dbo.Contest", t => t.Contest_Id1)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Contest_Id)
                .Index(t => t.Contest_Id1);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ContestEnrollmentPrice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContestId = c.Guid(nullable: false),
                        DogClassId = c.Guid(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contest", t => t.ContestId, cascadeDelete: true)
                .ForeignKey("dbo.DogClass", t => t.DogClassId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .Index(t => t.ContestId)
                .Index(t => t.DogClassId)
                .Index(t => t.UserProcessingId)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Contest",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        PublicationDate = c.DateTime(nullable: false),
                        EnrollmentStartDate = c.DateTime(nullable: false),
                        EnrollmentEndDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Opened2Enrollment = c.Boolean(nullable: false),
                        ContestResult = c.Boolean(nullable: false),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .Index(t => t.UserProcessingId);
            
            CreateTable(
                "dbo.Dog",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Pedigree = c.String(),
                        Name = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        FatherId = c.Guid(),
                        MotherId = c.Guid(),
                        DogSexId = c.Guid(nullable: false),
                        DogBreedId = c.Guid(nullable: false),
                        CategoryId = c.Guid(),
                        ContestActive = c.Boolean(nullable: false),
                        OwnerId = c.String(maxLength: 128),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                        Pai = c.Guid(),
                        Mae = c.Guid(),
                        Contest_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DogCategory", t => t.CategoryId)
                .ForeignKey("dbo.DogBreed", t => t.DogBreedId, cascadeDelete: true)
                .ForeignKey("dbo.DogSex", t => t.DogSexId, cascadeDelete: true)
                .ForeignKey("dbo.Dog", t => t.Pai)
                .ForeignKey("dbo.Dog", t => t.Mae)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .ForeignKey("dbo.Contest", t => t.Contest_Id)
                .Index(t => t.DogSexId)
                .Index(t => t.DogBreedId)
                .Index(t => t.CategoryId)
                .Index(t => t.OwnerId)
                .Index(t => t.UserProcessingId)
                .Index(t => t.Pai)
                .Index(t => t.Mae)
                .Index(t => t.Contest_Id);
            
            CreateTable(
                "dbo.DogSex",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        Sigla = c.String(maxLength: 1),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .Index(t => t.UserProcessingId);
            
            CreateTable(
                "dbo.ContestEnrollment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SequencialNumber = c.Int(nullable: false),
                        DogId = c.Guid(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        ContestId = c.Guid(nullable: false),
                        tokenPaiment = c.String(),
                        Price = c.Double(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        PricePaid = c.Double(nullable: false),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contest", t => t.ContestId, cascadeDelete: true)
                .ForeignKey("dbo.Dog", t => t.DogId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .Index(t => t.DogId)
                .Index(t => t.ContestId)
                .Index(t => t.UserProcessingId)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.ContestShelter",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContestId = c.Guid(nullable: false),
                        Sold = c.Boolean(nullable: false),
                        Price = c.Double(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        Street = c.String(),
                        Appartment = c.String(),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contest", t => t.ContestId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .Index(t => t.ContestId)
                .Index(t => t.UserProcessingId)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.ContestShelterPrice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContestShelterId = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContestShelter", t => t.ContestShelterId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .Index(t => t.ContestShelterId)
                .Index(t => t.UserProcessingId);
            
            CreateTable(
                "dbo.DogClass",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        DogClassParentId = c.Guid(),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DogClass", t => t.DogClassParentId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .Index(t => t.DogClassParentId)
                .Index(t => t.UserProcessingId);
            
            CreateTable(
                "dbo.DogAge",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        StartMonth = c.Int(nullable: false),
                        EndMonth = c.Int(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .Index(t => t.UserProcessingId);
            
            CreateTable(
                "dbo.DogType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserProcessingId)
                .Index(t => t.UserProcessingId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ApplicationUserExt",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 255),
                        DataDeNascimento = c.DateTime(nullable: false),
                        Endereco = c.String(nullable: false, maxLength: 255),
                        Numero = c.String(nullable: false, maxLength: 10),
                        Complemento = c.String(maxLength: 60),
                        Bairro = c.String(nullable: false, maxLength: 60),
                        Cidade = c.String(nullable: false, maxLength: 80),
                        Estado = c.String(nullable: false, maxLength: 50),
                        CEP = c.String(nullable: false, maxLength: 15),
                        TelefoneFixo = c.String(maxLength: 30),
                        TelefoneCelular = c.String(nullable: false, maxLength: 30),
                        NaoPossuiCPF = c.Boolean(nullable: false),
                        CPF = c.String(maxLength: 30),
                        NaoPossuiRG = c.Boolean(nullable: false),
                        RG = c.String(maxLength: 30),
                        OutroDocumento = c.Boolean(nullable: false),
                        OutroDocumentoDescription = c.String(maxLength: 30),
                        OutroDocumentoNumero = c.String(maxLength: 30),
                        AceitaComunicacaoPorEmail = c.Boolean(nullable: false),
                        AceitaComunicacaoContestsPorEmail = c.Boolean(nullable: false),
                        AceitaComunicacaoContestsResultadoPorEmail = c.Boolean(nullable: false),
                        UserProcessingId = c.String(maxLength: 128),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DogBreedDogCategory",
                c => new
                    {
                        DogBreed_Id = c.Guid(nullable: false),
                        DogCategory_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DogBreed_Id, t.DogCategory_Id })
                .ForeignKey("dbo.DogBreed", t => t.DogBreed_Id, cascadeDelete: true)
                .ForeignKey("dbo.DogCategory", t => t.DogCategory_Id, cascadeDelete: true)
                .Index(t => t.DogBreed_Id)
                .Index(t => t.DogCategory_Id);
            
            CreateTable(
                "dbo.DogAgeDogClass",
                c => new
                    {
                        DogAge_Id = c.Guid(nullable: false),
                        DogClass_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DogAge_Id, t.DogClass_Id })
                .ForeignKey("dbo.DogAge", t => t.DogAge_Id, cascadeDelete: true)
                .ForeignKey("dbo.DogClass", t => t.DogClass_Id, cascadeDelete: true)
                .Index(t => t.DogAge_Id)
                .Index(t => t.DogClass_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserExt", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.DogType", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestEnrollmentPrice", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestEnrollmentPrice", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestEnrollmentPrice", "DogClassId", "dbo.DogClass");
            DropForeignKey("dbo.DogClass", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DogClass", "DogClassParentId", "dbo.DogClass");
            DropForeignKey("dbo.DogAge", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DogAgeDogClass", "DogClass_Id", "dbo.DogClass");
            DropForeignKey("dbo.DogAgeDogClass", "DogAge_Id", "dbo.DogAge");
            DropForeignKey("dbo.ContestEnrollmentPrice", "ContestId", "dbo.Contest");
            DropForeignKey("dbo.Contest", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestShelter", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestShelter", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestShelterPrice", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestShelterPrice", "ContestShelterId", "dbo.ContestShelter");
            DropForeignKey("dbo.ContestShelter", "ContestId", "dbo.Contest");
            DropForeignKey("dbo.AspNetUsers", "Contest_Id1", "dbo.Contest");
            DropForeignKey("dbo.AspNetUsers", "Contest_Id", "dbo.Contest");
            DropForeignKey("dbo.ContestEnrollment", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestEnrollment", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ContestEnrollment", "DogId", "dbo.Dog");
            DropForeignKey("dbo.ContestEnrollment", "ContestId", "dbo.Contest");
            DropForeignKey("dbo.Dog", "Contest_Id", "dbo.Contest");
            DropForeignKey("dbo.Dog", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Dog", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Dog", "Mae", "dbo.Dog");
            DropForeignKey("dbo.Dog", "Pai", "dbo.Dog");
            DropForeignKey("dbo.Dog", "DogSexId", "dbo.DogSex");
            DropForeignKey("dbo.DogSex", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Dog", "DogBreedId", "dbo.DogBreed");
            DropForeignKey("dbo.Dog", "CategoryId", "dbo.DogCategory");
            DropForeignKey("dbo.DogCategory", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DogBreed", "UserProcessingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DogBreedDogCategory", "DogCategory_Id", "dbo.DogCategory");
            DropForeignKey("dbo.DogBreedDogCategory", "DogBreed_Id", "dbo.DogBreed");
            DropIndex("dbo.DogAgeDogClass", new[] { "DogClass_Id" });
            DropIndex("dbo.DogAgeDogClass", new[] { "DogAge_Id" });
            DropIndex("dbo.DogBreedDogCategory", new[] { "DogCategory_Id" });
            DropIndex("dbo.DogBreedDogCategory", new[] { "DogBreed_Id" });
            DropIndex("dbo.ApplicationUserExt", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.DogType", new[] { "UserProcessingId" });
            DropIndex("dbo.DogAge", new[] { "UserProcessingId" });
            DropIndex("dbo.DogClass", new[] { "UserProcessingId" });
            DropIndex("dbo.DogClass", new[] { "DogClassParentId" });
            DropIndex("dbo.ContestShelterPrice", new[] { "UserProcessingId" });
            DropIndex("dbo.ContestShelterPrice", new[] { "ContestShelterId" });
            DropIndex("dbo.ContestShelter", new[] { "Owner_Id" });
            DropIndex("dbo.ContestShelter", new[] { "UserProcessingId" });
            DropIndex("dbo.ContestShelter", new[] { "ContestId" });
            DropIndex("dbo.ContestEnrollment", new[] { "Owner_Id" });
            DropIndex("dbo.ContestEnrollment", new[] { "UserProcessingId" });
            DropIndex("dbo.ContestEnrollment", new[] { "ContestId" });
            DropIndex("dbo.ContestEnrollment", new[] { "DogId" });
            DropIndex("dbo.DogSex", new[] { "UserProcessingId" });
            DropIndex("dbo.Dog", new[] { "Contest_Id" });
            DropIndex("dbo.Dog", new[] { "Mae" });
            DropIndex("dbo.Dog", new[] { "Pai" });
            DropIndex("dbo.Dog", new[] { "UserProcessingId" });
            DropIndex("dbo.Dog", new[] { "OwnerId" });
            DropIndex("dbo.Dog", new[] { "CategoryId" });
            DropIndex("dbo.Dog", new[] { "DogBreedId" });
            DropIndex("dbo.Dog", new[] { "DogSexId" });
            DropIndex("dbo.Contest", new[] { "UserProcessingId" });
            DropIndex("dbo.ContestEnrollmentPrice", new[] { "Owner_Id" });
            DropIndex("dbo.ContestEnrollmentPrice", new[] { "UserProcessingId" });
            DropIndex("dbo.ContestEnrollmentPrice", new[] { "DogClassId" });
            DropIndex("dbo.ContestEnrollmentPrice", new[] { "ContestId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Contest_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "Contest_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.DogBreed", new[] { "UserProcessingId" });
            DropIndex("dbo.DogCategory", new[] { "UserProcessingId" });
            DropTable("dbo.DogAgeDogClass");
            DropTable("dbo.DogBreedDogCategory");
            DropTable("dbo.ApplicationUserExt");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.DogType");
            DropTable("dbo.DogAge");
            DropTable("dbo.DogClass");
            DropTable("dbo.ContestShelterPrice");
            DropTable("dbo.ContestShelter");
            DropTable("dbo.ContestEnrollment");
            DropTable("dbo.DogSex");
            DropTable("dbo.Dog");
            DropTable("dbo.Contest");
            DropTable("dbo.ContestEnrollmentPrice");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.DogBreed");
            DropTable("dbo.DogCategory");
        }
    }
}
