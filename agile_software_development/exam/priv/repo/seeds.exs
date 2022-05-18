# Script for populating the database. You can run it as:
#
#     mix run priv/repo/seeds.exs
#
# Inside the script, you can read and write to any of your
# repositories directly:
#
#     Exam.Repo.insert!(%Exam.SomeSchema{})
#
# We recommend using the bang functions (`insert!`, `update!`
# and so on) as they will fail if something goes wrong.

alias Exam.Repo
alias Exam.Account.User
alias Exam.Resource.Loan

defmodule Exam.Seeds do
  import Ecto.Query

  def store_user(attrs) do
    changeset = User.changeset(%User{}, attrs)
    Repo.insert!(changeset)
  end

  def store_loan(attrs) do
    query =
      from u in User,
        select: u.id
    users = Repo.all(query)

    changeset = Loan.changeset(%Loan{}, attrs |> Map.put("user_id", Enum.random(users)))
    Repo.insert!(changeset)
  end
end

[
  %{full_name: "Test", email: "test@test.com", monthly_income: 1000},
  %{full_name: "Test1", email: "test1@test.com", monthly_income: 3000},
  %{full_name: "Test2", email: "test2@test.com", monthly_income: 2500},
  %{full_name: "Test3", email: "test3@test.com", monthly_income: 600}
] |> Enum.map(fn user -> Exam.Seeds.store_user(user) end)

Exam.Seeds.store_loan(%{
  "loan_term" => 2,
  "date_taken" => NaiveDateTime.local_now(),
  "loan_amount" => 205
})
