defmodule ExamWeb.LoanControllerTest do
  use ExamWeb.ConnCase

  import Ecto.Query

  alias Exam.Repo
  alias Exam.Account.User
  alias Exam.Resource.Loan

  def user_fixture(user) do
    changeset = User.changeset(%User{}, user)
    Repo.insert!(changeset)
  end

  describe "request loan" do
    setup %{conn: conn} do
      [
        %{full_name: "Test", email: "test@test.com", monthly_income: 1000},
        %{full_name: "Test1", email: "test1@test.com", monthly_income: 3000},
        %{full_name: "Test2", email: "test2@test.com", monthly_income: 2500},
        %{full_name: "Test3", email: "test3@test.com", monthly_income: 600}
      ] |> Enum.map(fn user -> user_fixture(user) end)

      query =
        from u in User,
        select: u.email
      users = Repo.all(query)

      loan_request = %{
        "loan_term" => "2",
        "loan_amount" => "100",
        "email" => Enum.random(users)
      }

      user_fixture(%{full_name: "Bad Test", email: "fegaeze@gmail.com", monthly_income: 1600})

      bad_request = %{
        "loan_term" => "2",
        "loan_amount" => "100",
        "email" => "fegaeze@gmail.com"
      }

      {
        :ok,
        %{
          conn: conn,
          loan: loan_request,
          bad: bad_request
        }
      }
    end

    test "POST /loans/new - New Request Created", %{conn: conn, loan: loan} do
      conn = post(conn, "/loans", %{loan: loan})

      user_query =
        from u in User,
        where: u.email == ^loan["email"],
        select: u.id

      user_id = Repo.one(user_query)

      loan_query =
        from l in Loan,
        where: l.user_id == ^user_id,
        select: l

      loan_db = Repo.one(loan_query)

      {term, ""} = Integer.parse(loan["loan_term"])
      {amount, ""} = Integer.parse(loan["loan_amount"])

      assert loan_db.loan_amount == amount + ((amount * 2.5) / 100)
      assert loan_db.user_id == user_id
      assert loan_db.loan_term == term
      assert html_response(conn, 200) =~ ~r/You have successfully requested a loan/
    end

    test "POST /loans/new - New Request Failed", %{conn: conn, bad: bad} do
      conn = post(conn, "/loans", %{loan: bad})
      conn = post(conn, "/loans", %{loan: bad})
      assert html_response(conn, 200) =~ ~r/Sorry, you are not eligible for the loan/
    end
  end
end
