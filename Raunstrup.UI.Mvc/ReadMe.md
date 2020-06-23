# **Migration**


Add-Migration WorkingTitleAdded -context RaunstrupContext -Project Raunstrup.Service.Infrastructure
Update-Database WorkingTitleAdded -context RaunstrupContext -Project Raunstrup.Service.Infrastructure

Add-Migration NewDatabase -context RaunstrupContext -Project Raunstrup.Service.Infrastructure
