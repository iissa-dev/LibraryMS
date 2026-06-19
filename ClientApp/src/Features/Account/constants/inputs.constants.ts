export const INPUTS = [
  {
    type: "text",
    placeholder: "Username",
    id: "username",
    gridOrder: "col-span-2",
    name: "userName",
  },
  {
    type: "text",
    placeholder: "First Name",
    id: "first-name",
    gridOrder: "col-span-2 md:col-span-1",
    name: "firstName",
  },
  {
    type: "text",
    placeholder: "Last Name",
    id: "last-name",
    gridOrder: "col-span-2 md:col-span-1",
    name: "lastName",
  },
  {
    type: "email",
    placeholder: "Email",
    id: "email",
    gridOrder: "col-span-2",
    name: "email",
  },
  {
    type: "password",
    placeholder: "Password",
    id: "password",
    gridOrder: "col-span-2 md:col-span-1",
    name: "password",
  },
  {
    type: "text",
    placeholder: "Address",
    id: "address",
    gridOrder: "col-span-2 md:col-span-1",
    name: "address",
  },
] as const;
