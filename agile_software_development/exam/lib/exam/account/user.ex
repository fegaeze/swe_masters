defmodule Exam.Account.User do
  use Ecto.Schema
  import Ecto.Changeset

  alias Exam.Resource.Loan

  schema "users" do
    field :email, :string
    field :full_name, :string
    field :monthly_income, :integer

    has_one :loan, Loan

    timestamps()
  end

  @doc false
  def changeset(user, attrs) do
    user
    |> cast(attrs, [:full_name, :email, :monthly_income])
    |> validate_required([:full_name, :email, :monthly_income])
    |> unique_constraint([:email])
  end
end
