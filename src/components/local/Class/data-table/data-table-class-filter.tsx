import { zodResolver } from "@hookform/resolvers/zod"
import { ListFilter } from "lucide-react"
import { useForm } from "react-hook-form"
import { toast } from "sonner"
import * as z from "zod"

import { FilterSchema } from "@/lib/schemas/class-filter"

import { Button } from "@/components/global/atoms/button"
import { Checkbox } from "@/components/global/atoms/checkbox"
import { DatePicker } from "@/components/global/atoms/date-picker"
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage
} from "@/components/global/atoms/form"
import MultipleSelector, {
  Option
} from "@/components/global/atoms/multiple-selector"
import {
  Popover,
  PopoverContent,
  PopoverTrigger
} from "@/components/global/atoms/popover"
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue
} from "@/components/global/atoms/select"

const OPTIONS: Option[] = [
  { label: "nextjs", value: "Nextjs" },
  { label: "Vite", value: "vite" },
  { label: "Nuxt", value: "nuxt" },
  { label: "Vue", value: "vue" },
  { label: "Remix", value: "remix" },
  { label: "Svelte", value: "svelte", disable: true },
  { label: "Angular", value: "angular", disable: true },
  { label: "Ember", value: "ember", disable: true },
  { label: "React", value: "react" },
  { label: "Gatsby", value: "gatsby", disable: true },
  { label: "Astro", value: "astro", disable: true }
]

export const DataTableClassFilter = () => {
  const form = useForm<z.infer<typeof FilterSchema>>({
    resolver: zodResolver(FilterSchema),
    defaultValues: {
      location: []
      // from: new Date(),
      // to: new Date(),
      // classTime: "Morning",
      // status: "Planning",
      // attendee: "Intern",
      // fsu: "",
      // trainer: ""
    }
  })

  const onSubmit = async (data: z.infer<typeof FilterSchema>) => {
    console.log("data", data)

    toast.success(JSON.stringify(data))
  }

  return (
    <Popover>
      <PopoverTrigger asChild>
        <Button
          variant="outline"
          size="sm"
          className="h-8 border-dashed border-gray-400"
        >
          <ListFilter className="mr-2 h-4 w-4" />
          Filter
        </Button>
      </PopoverTrigger>
      <PopoverContent className="flex w-[900px] flex-col" align="start">
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-10">
            <div className="grid grid-cols-2 gap-4">
              <div className="col-span-1 space-y-2">
                <h2 className="font-semibold">Class location</h2>
                <FormField
                  control={form.control}
                  name="location"
                  render={({ field }) => (
                    <FormItem>
                      <FormControl>
                        <MultipleSelector
                          value={field.value}
                          onChange={field.onChange}
                          defaultOptions={OPTIONS}
                          placeholder="Select frameworks you like..."
                          emptyIndicator={
                            <p className="text-center text-lg leading-10 text-gray-600 dark:text-gray-400">
                              no results found.
                            </p>
                          }
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
              <div className="col-span-1 space-y-2">
                <h2 className="font-semibold">Class time frame</h2>
                <div className="flex items-center space-x-2">
                  <span>from</span>
                  <DatePicker />
                  <span>to</span>
                  <DatePicker />
                </div>
              </div>
            </div>
            <div className="grid grid-cols-3 gap-2">
              <div className="col-span-1">
                <div className="flex items-start space-x-4">
                  <h2 className="font-semibold">Class time</h2>
                  <div className="flex flex-col space-y-2">
                    {/* CHECKBOX LIST */}
                    <div className="flex items-center space-x-2">
                      <Checkbox id="terms" />
                      <label
                        htmlFor="terms"
                        className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                      >
                        Morning
                      </label>
                    </div>
                    <div className="flex items-center space-x-2">
                      <Checkbox id="terms" />
                      <label
                        htmlFor="terms"
                        className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                      >
                        Noon
                      </label>
                    </div>
                    <div className="flex items-center space-x-2">
                      <Checkbox id="terms" />
                      <label
                        htmlFor="terms"
                        className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                      >
                        Night
                      </label>
                    </div>
                    <div className="flex items-center space-x-2">
                      <Checkbox id="terms" />
                      <label
                        htmlFor="terms"
                        className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                      >
                        Online
                      </label>
                    </div>
                  </div>
                </div>
              </div>
              <div className="col-span-1">
                <div className="flex items-start space-x-4">
                  <h2 className="font-semibold">Status</h2>
                  <div className="flex flex-col space-y-2">
                    {/* CHECKBOX LIST */}
                    <div className="flex items-center space-x-2">
                      <Checkbox id="terms" />
                      <label
                        htmlFor="terms"
                        className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                      >
                        Planning
                      </label>
                    </div>
                    <div className="flex items-center space-x-2">
                      <Checkbox id="terms" />
                      <label
                        htmlFor="terms"
                        className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                      >
                        Opening
                      </label>
                    </div>
                    <div className="flex items-center space-x-2">
                      <Checkbox id="terms" />
                      <label
                        htmlFor="terms"
                        className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                      >
                        Closed
                      </label>
                    </div>
                  </div>
                </div>
              </div>
              <div className="col-span-1">
                <div className="flex items-start space-x-4">
                  <h2 className="font-semibold">Attendee</h2>
                  <div className="flex flex-col space-y-2">
                    {/* CHECKBOX LIST */}
                    <div className="flex items-center space-x-2">
                      <Checkbox id="terms" />
                      <label
                        htmlFor="terms"
                        className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                      >
                        Intern
                      </label>
                    </div>
                    <div className="flex items-center space-x-2">
                      <Checkbox id="terms" />
                      <label
                        htmlFor="terms"
                        className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                      >
                        Fresher
                      </label>
                    </div>
                    <div className="flex items-center space-x-2">
                      <Checkbox id="terms" />
                      <label
                        htmlFor="terms"
                        className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                      >
                        Online fee-fresher
                      </label>
                    </div>
                    <div className="flex items-center space-x-2">
                      <Checkbox id="terms" />
                      <label
                        htmlFor="terms"
                        className="text-sm font-medium leading-none peer-disabled:cursor-not-allowed peer-disabled:opacity-70"
                      >
                        Offline fee-fresher
                      </label>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div className="grid grid-cols-2 gap-4">
              <div className="col-span-1 flex items-center space-x-4">
                <h2 className="font-semibold">FSU</h2>
                <Select>
                  <SelectTrigger className="w-full">
                    <SelectValue placeholder="Select a fruit" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectGroup>
                      <SelectLabel>Fruits</SelectLabel>
                      <SelectItem value="apple">Apple</SelectItem>
                      <SelectItem value="banana">Banana</SelectItem>
                      <SelectItem value="blueberry">Blueberry</SelectItem>
                      <SelectItem value="grapes">Grapes</SelectItem>
                      <SelectItem value="pineapple">Pineapple</SelectItem>
                    </SelectGroup>
                  </SelectContent>
                </Select>
              </div>
              <div className="col-span-1 flex items-center space-x-4">
                <h2 className="font-semibold">Trainer</h2>
                <Select>
                  <SelectTrigger className="w-full">
                    <SelectValue placeholder="Select a fruit" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectGroup>
                      <SelectLabel>Fruits</SelectLabel>
                      <SelectItem value="apple">Apple</SelectItem>
                      <SelectItem value="banana">Banana</SelectItem>
                      <SelectItem value="blueberry">Blueberry</SelectItem>
                      <SelectItem value="grapes">Grapes</SelectItem>
                      <SelectItem value="pineapple">Pineapple</SelectItem>
                    </SelectGroup>
                  </SelectContent>
                </Select>
              </div>
            </div>
            <div className="flex items-center justify-end space-x-2">
              <Button type="button" onClick={() => form.reset()}>
                Clear
              </Button>
              <Button type="submit">Search</Button>
            </div>
          </form>
        </Form>
      </PopoverContent>
    </Popover>
  )
}
