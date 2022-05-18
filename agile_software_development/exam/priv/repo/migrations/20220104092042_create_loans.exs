defmodule Exam.Repo.Migrations.CreateLoans do
  use Ecto.Migration

  def change do
    create table(:loans) do
      add :user_id, references(:users)
      add :loan_amount, :float
      add :loan_term, :integer
      add :date_taken, :naive_datetime

      timestamps()
    end
  end
end
