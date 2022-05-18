defmodule ExamWeb.LoanController do
  use ExamWeb, :controller

  import Ecto.Query

  alias Exam.Repo
  alias Exam.Account.User
  alias Exam.Resource.Loan

  def new(conn, _params) do
    changeset = Loan.changeset(%Loan{}, %{})
    render(conn, "new.html", changeset: changeset)
  end

  def create(conn, %{"loan" => loan}) do

    {term, ""} = Integer.parse(loan["loan_term"])
    {amount, ""} = Integer.parse(loan["loan_amount"])
    {interest, total_amount} = calculate(amount, term)

    user_query =
      from u in User,
      where: u.email == ^loan["email"],
      select: u

    user = Repo.one(user_query)
    monthly = amount / (12 * term)

    cond do
      (term < 1 or term > 15) ->
        changeset = Loan.changeset(%Loan{}, %{})
        conn
        |> put_flash(:info, "Sorry, you are not eligible for the loan")
        |> render("new.html", changeset: changeset)
      (user == nil) ->
          changeset = Loan.changeset(%Loan{}, %{})
          conn
          |> put_flash(:info, "Sorry, you are not eligible for the loan")
          |> render("new.html", changeset: changeset)
      (monthly > (user.monthly_income * 40) / 100) ->
        changeset = Loan.changeset(%Loan{}, %{})
        conn
        |> put_flash(:info, "Sorry, you are not eligible for the loan")
        |> render("new.html", changeset: changeset)
      true ->
        user_query =
          from u in User,
          where: u.email == ^loan["email"],
          select: u.id

        user_id = Repo.one(user_query)

        query =
          from l in Loan,
          where: l.user_id == ^user_id,
          select: l

        loan = Repo.all(query)

        case length(loan) >= 1 do
          true ->
            changeset = Loan.changeset(%Loan{}, %{})

            conn
            |> put_flash(:info, "Sorry, you are not eligible for the loan")
            |> render("new.html", changeset: changeset)
          false ->

            loan_todb = %{
              date_taken: NaiveDateTime.local_now(),
              loan_term: term,
              loan_amount: total_amount,
              user_id: user_id
            }

            loan_changeset = Loan.changeset(%Loan{}, loan_todb)
            transaction = Repo.insert(loan_changeset)

            case transaction do
              {:ok, params} ->
                changeset = Loan.changeset(%Loan{}, %{})

                conn
                |> put_flash(:info, "You have successfully requested a loan. You are to pay #{monthly} euros within #{params.loan_term} years at #{interest}%")
                |> render("new.html", changeset: changeset)
              {:error, _name, changeset, %{}} ->
                render(conn, "new.html", changeset: changeset)
            end
        end
    end
  end

  defp calculate(amount, term) do
    cond do
      (term >= 1) or (term < 5) ->
        {2.5, amount + (amount * 2.5) / 100}
      (term >= 5) or (term < 10)  ->
        {3.5, amount + (amount * 3.5) / 100}
      true  ->
        {5, amount + (amount * 5) / 100}
    end
  end
end
