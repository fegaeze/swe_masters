defmodule Exam.Resource.Loan do
  use Ecto.Schema
  import Ecto.Changeset

  alias Exam.Account.User

  schema "loans" do
    field :date_taken, :naive_datetime
    field :loan_amount, :float
    field :loan_term, :integer

    belongs_to :user, User

    timestamps()
  end

  @doc false
  def changeset(loan, attrs) do
    loan
    |> cast(attrs, [:user_id, :loan_amount, :loan_term, :date_taken])
    |> validate_required([:loan_amount, :loan_term, :date_taken])
  end
end
