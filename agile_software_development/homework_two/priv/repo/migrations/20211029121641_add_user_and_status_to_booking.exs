defmodule BookYourTaxi.Repo.Migrations.AddUserAndStatusToBooking do
  use Ecto.Migration

  def change do
    alter table(:bookings) do
      modify :status, :string, default: "open"
      add :user_id, references(:users)
    end
  end
end
