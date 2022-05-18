defmodule BookYourTaxi.Repo.Migrations.AddCompletedRidesToTaxi do
  use Ecto.Migration

  def change do
    alter table(:taxis) do
      add :completed_rides, :integer, default: 0
    end
  end
end
