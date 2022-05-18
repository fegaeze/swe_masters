defmodule WhiteBreadContext do
  use WhiteBread.Context
  use Hound.Helpers

  import Ecto.Query

  alias Exam.Repo
  alias Exam.Account.User
  alias Exam.Resource.Loan


  feature_starting_state fn  ->
    Application.ensure_all_started(:hound)
    %{}
  end

  scenario_starting_state fn _state ->
    Hound.start_session
    %{}
  end

  scenario_finalize fn _status, _state ->
    Hound.end_session
  end

  given_ ~r/^that I am one of several registered users with the following emails$/, fn state, %{table_data: users} ->
    users
    |> Enum.map(fn user -> User.changeset(%User{}, user) end)
    |> Enum.each(fn changeset -> Repo.insert!(changeset) end)

    query =
      from u in User,
        where: u.email == "test1@test.com",
        select: u.id

    users = Repo.all(query)
    {:ok, state |> Map.put(:user_id, Enum.random(users))}
  end

  and_ ~r/^there is already a loan for one of the users in the system$/, fn state, %{table_data: loans} ->
    loans
    |> Enum.map(fn loan -> Loan.changeset(%Loan{}, loan |> Map.put(:date_taken, NaiveDateTime.local_now()) |> Map.put(:user_id, state.user_id)) end)
    |> Enum.each(fn changeset -> Repo.insert!(changeset) end)
    {:ok, state}
  end

  and_ ~r/^I want to request for a new loan$/, fn state ->
    navigate_to "/loans/new"
    {:ok, state}
  end

  and_ ~r/^I enter the my email "(?<email>[^"]+)" and the loan amount "(?<loan_amount>[^"]+)" and the loan term "(?<loan_term>[^"]+)"$/,
  fn state, %{email: email, loan_amount: loan_amount, loan_term: loan_term} ->
    fill_field({:id, "email"}, email)
    fill_field({:id, "loan_amount"}, loan_amount)
    fill_field({:id, "loan_term"}, loan_term)
    {:ok, state}
  end

  when_ ~r/^I click the submit button$/, fn state ->
    click({:id, "submit_button"})
    {:ok, state}
  end

  then_ ~r/^I should receive a confirmation message including the interest ratio, the monthly amount to be paid, and the number of years to complete the payment$/, fn state ->
    assert visible_in_page? ~r/You have successfully requested a loan. You are to pay 8.54 euros within 2 years at 2.5%/
    {:ok, state}
  end

end
