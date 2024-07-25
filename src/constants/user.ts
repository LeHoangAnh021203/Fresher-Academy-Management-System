import { User } from "@/lib/types"

export const userData: User[] = [
  {
    userId: "1",
    name: "John Doe",
    email: "john.doe@example.com",
    password: "password123",
    dob: "01-01-1999",
    role: "Admin",
    gender: "male",
    status: "Active",
    createdBy: { name: "Admin" },
    createdOn: "02-14-2022",
    modifiedBy: { name: "Admin" },
    modifiedOn: "02-14-2022"
  },
  {
    userId: "2",
    name: "Jane Doe",
    email: "jane.doe@example.com",
    password: "securepass",
    role: "Trainer",
    dob: "05-15-1985",
    gender: "female",
    status: "Active",
    createdBy: { name: "Admin" },
    createdOn: "02-15-2022",
    modifiedBy: { name: "Editor" },
    modifiedOn: "02-16-2022"
  },
  {
    userId: "3",
    name: "Alice Johnson",
    email: "alice.johnson@example.com",
    password: "alicepass123",
    dob: "08-22-1995",
    role: "Admin",
    gender: "female",
    status: "Inactive",
    createdBy: { name: "Editor" },
    createdOn: "02-16-2022",
    modifiedBy: { name: "Admin" },
    modifiedOn: "02-17-2022"
  },
  {
    userId: "4",
    name: "Bob Smith",
    email: "bob.smith@example.com",
    password: "bobssecurepass",
    dob: "04-10-1980",
    role: "Admin",
    gender: "male",
    status: "Active",
    createdBy: { name: "Admin" },
    createdOn: "02-18-2022",
    modifiedBy: { name: "Editor" },
    modifiedOn: "02-19-2022"
  },
  {
    userId: "5",
    name: "Eva Rodriguez",
    email: "eva.rodriguez@example.com",
    password: "evapassword",
    dob: "12-05-1998",
    role: "Trainer",
    gender: "female",
    status: "Active",
    createdBy: { name: "Admin" },
    createdOn: "02-20-2022",
    modifiedBy: { name: "Admin" },
    modifiedOn: "02-21-2022"
  }
]

export const CREATE_PERMISSION = [3, 4]
export const MODIFY_PERMISSION = [2, 4]
