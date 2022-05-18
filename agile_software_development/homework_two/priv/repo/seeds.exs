# Script for populating the database. You can run it as:
#
#     mix run priv/repo/seeds.exs
#
# Inside the script, you can read and write to any of your
# repositories directly:
#
#     BookYourTaxi.Repo.insert!(%BookYourTaxi.SomeSchema{})
#
# We recommend using the bang functions (`insert!`, `update!`
# and so on) as they will fail if something goes wrong.

alias BookYourTaxi.{Repo, Accounts.User, Sales.Taxi}

[
  %{full_name: "Fred Flintstone", email: "fred@abc.com", password: "parool", age: 23, role: "customer"},
  %{full_name: "Barney Rubble", email: "barney@123.com", password: "parool", age: 35, role: "customer"},
  %{full_name: "Jamie Dukes", email: "jamie@xyz.com", password: "parool", age: 62, role: "driver"},
  %{full_name: "Elliot Hale", email: "elliot@stuv.com", password: "parool", age: 42, role: "driver"},
]
|> Enum.map(fn user_data -> User.changeset(%User{}, user_data) end)
|> Enum.each(fn changeset -> Repo.insert!(changeset) end)

# [
#   %{number_of_seats: 4, location: "Teater Vanemuine", status: "available", rate: 0.90, user_id: 3, completed_rides: 1},
#   %{number_of_seats: 6, location: "ERM", status: "available", rate: 1.10, user_id: 4, completed_rides: 0},
# ]
# |> Enum.map(fn taxi_data -> Taxi.changeset(%Taxi{}, taxi_data) end)
# |> Enum.each(fn changeset -> Repo.insert!(changeset) end)
