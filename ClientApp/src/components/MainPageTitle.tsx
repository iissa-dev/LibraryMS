type Params = {
  Title: string;
  Description: string;
};
const MainPageTitle = ({ Title, Description }: Params) => {
  return (
    <div>
      <h1 className="main-title text-[30px] md:text-[45px]">{Title}</h1>
      <p className="text-text">{Description}</p>
    </div>
  );
};

export default MainPageTitle;
