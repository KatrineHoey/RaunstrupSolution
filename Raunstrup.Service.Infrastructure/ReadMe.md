# **Migration**


Add-Migration InitializeCreate -context RaunstrupContext -Project Raunstrup.Service.Infrastructure
Update-Database InitializeCreate -context RaunstrupContext -Project Raunstrup.Service.Infrastructure

Add-Migration CampaignAdded -context RaunstrupContext -Project Raunstrup.Service.Infrastructure
Update-Database CampaignAdded -context RaunstrupContext -Project Raunstrup.Service.Infrastructure

Add-Migration OfferAdded -context RaunstrupContext
Update-Database OfferAdded -context RaunstrupContext

Add-Migration IsProjectLeaderAdded -context RaunstrupContext -Project Raunstrup.Service.Infrastructure

Add-Migration updating -context RaunstrupContext -Project Raunstrup.Service.Infrastructure


Add-Migration AssignedItemMeasuringUnitAdded -context RaunstrupContext -Project Raunstrup.Service.Infrastructure
Update-Database AssignedItemMeasuringUnitAdded -context RaunstrupContext -Project Raunstrup.Service.Infrastructure

Add-Migration OfferUpdateMigration -context RaunstrupContext -Project Raunstrup.Service.Infrastructure
Update-Database OfferUpdateMigration -context RaunstrupContext -Project Raunstrup.Service.Infrastructure

Add-Migration RettetFejl -context RaunstrupContext -Project Raunstrup.Service.Infrastructure
Update-Database RettetFejl -context RaunstrupContext -Project Raunstrup.Service.Infrastructure

Add-Migration ProfessionAdded -context RaunstrupContext -Project Raunstrup.Service.Infrastructure
Update-Database ProfeAdd-Migration NewDatabase -context RaunstrupContext -Project Raunstrup.Service.Infrastructure


Add-Migration discount -context RaunstrupContext -Project Raunstrup.Service.Infrastructure
Update-Database discount -context RaunstrupContext -Project Raunstrup.Service.Infrastructure
