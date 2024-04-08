# Getting started

## Prerequisites
* dotnet cli
* docker desktop

## Steps
1. Create a `.env` in the root directoy and copy the fields from `.env.sample`
2. Enter a password, and generate an `APP_SECRET` from here: https://randomkeygen.com/ (504-bit WPA Key)
3. Run `docker-compose build`
4. Run `docker-compose up -d`
5. From `.\SchedulerPro.DAL` run `dotnet ef database update -s ../SchedulerPro.API`
6. Open `http://localhost:5173/`
7. Create a new user

# Improvements
* window.location.reload - at the moment I use this to refresh the page when doing an api call the will change the data. Instead I could use something like ReactQuery and ContextProvider to call refetches to refresh the state
* fetches - because most of the fetches I do are similar, I could write a wrapper to standardize and reduce code being repeated
* scss - I could generalise a lot of what I've got
* clean up deletes - If you delete a user, they are removed from all meetings. If there are any meetings left afterwards that have <2 attendees, that meeting needs to be deleted, too
* React service - Add tests to ensure the code is working as expected

# Notes
* Creating a migration: From `.\SchedulerPro.DAL` run `dotnet ef migrations add initialCreate -s ../SchedulerPro.API`