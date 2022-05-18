defmodule BookYourTaxi.Repo.Migrations.CreateTaxis do
  use Ecto.Migration

  def change do
    create table(:taxis) do
      add :number_of_seats, :integer
      add :location, :string
      add :status, :string
      add :rate, :float

      timestamps()
    end
  end
end
